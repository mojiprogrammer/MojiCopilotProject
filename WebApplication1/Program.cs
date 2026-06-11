
using DanaCopilot.AI.LLM;
using DanaCopilot.AI.OCR;
using DanaCopilot.Application;
using DanaCopilot.Application.Contracts.AI;
using DanaCopilot.Application.Contracts.Chat;
using DanaCopilot.Application.Contracts.Knowledge;
using DanaCopilot.Application.Contracts.Repositories.Interfaces;
using DanaCopilot.Application.Contracts.Retrieval;
using DanaCopilot.Application.Services;
using DanaCopilot.Application.UseCases.Copilot;
using DanaCopilot.BackgroundJobs.Services;
using DanaCopilot.Infrastructure.Security;
using DanaCopilot.Infrastructure.Services;
using DanaCopilot.Persistence;
using DanaCopilot.Persistence.Repositories;
using DanaCopilot.Persistence.Repositories.Interfaces;
using DanaCopilot.Retrieval.Context;
using DanaCopilot.Retrieval.Contracts;
using DanaCopilot.Retrieval.Scoring;
using DanaCopilot.Retrieval.Search;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Moji.Controllers;
using Moji.DataService;
using Moji.DataService.Repositories;
using Moji.DataService.Repositories.Interfaces;
using Moji.DataService.Repositories.ModelRepositories;
using Moji.Services.Helper;
using Moji.Services.Helper.Implement;
using Moji.Services.Interfaces;
using Moji.Services.MLServices;
using Moji.Services.Models;
using Moji.Services.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

// Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Moji API",
        Version = "v1",
        Description = "Moji Copilot Application API with JWT Authentication and Price Prediction"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Database and Dependency Injection
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddDbContext<DanaAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MojiCopilotConnection"));
});

//builder.Services.AddScoped<DanaAppDbContext>();

// Register Offline DbContext (SQLite) - For prediction module
var offlineConnectionString = builder.Configuration.GetConnectionString("OfflineConnection")
    ?? "Data Source=offline_prediction.db";
builder.Services.AddDbContext<OfflineDbContext>(options =>
{
    options.UseSqlite(offlineConnectionString);
}, ServiceLifetime.Scoped);

// Register repositories (Original ones)
builder.Services.AddScoped<IUserRepositoryDataService, UserRepositoryDataService>();
builder.Services.AddScoped<IUserProfileRepositoryDataService, UserProfileRepositoryDataService>();

// Register prediction repositories and services
builder.Services.AddScoped<IPredictionRepository, PredictionRepository>();
builder.Services.AddScoped<IPredictionService, PredictionService>();

builder.Services.AddScoped<IMenuRepositoryDataService, MenuRepositoryDataService>();
builder.Services.AddScoped<IMenuService, MenuService>();

builder.Services.AddScoped<IUserRoleRepositoryDataService, UserRoleRepositoryDataService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

builder.Services.AddScoped<IFileUploadHelper, FileUploadHelper>();

// Register background service for auto-training ML models
builder.Services.AddHostedService<ModelTrainingBackgroundService>();
builder.Services.AddHttpClient();

//DanaCopilot Services

builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IKnowledgeGapRepository, KnowledgeGapRepository>();
builder.Services.AddScoped<IDocumentChunkRepository, DocumentChunkRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAnswerSourceRepository, AnswerSourceRepository>();

builder.Services.AddScoped<IFileStorage, LocalFileStorage>();
builder.Services.AddScoped<IDanaPasswordHasher, DanaPasswordHasher>();
builder.Services.AddScoped<IConversationService, ConversationService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IKnowledgeGapService, KnowledgeGapService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<ISqlSearchService, SqlSearchService>();
builder.Services.AddScoped<IRetrievalService, RetrievalService>();
builder.Services.AddScoped<IDocumentProcessingService, DocumentProcessingService>();
builder.Services.AddScoped<IOcrService, TesseractOcrService>();
builder.Services.AddScoped<ITextChunker, TextChunker>();

builder.Services.AddScoped<PromptBuilder>();
builder.Services.AddScoped<ILocalLlm, OllamaLlm>();

builder.Services.AddScoped<ICopilotOrchestrator, CopilotOrchestrator>();


builder.Services.AddHttpClient<ILocalLlm, OllamaLlm>(client =>
                       {
                           client.BaseAddress =new Uri("http://localhost:11434");
                       });

builder.Services.AddScoped<ContextBuilder>();

builder.Services.AddScoped<ConfidenceScorer>();

// Configure prediction settings from appsettings.json
builder.Services.Configure<PredictionSettings>(
    builder.Configuration.GetSection("PredictionSettings"));

var jwtSecret = builder.Configuration["AppSettings:Jwt:Secret"];
if (string.IsNullOrEmpty(jwtSecret))
{
    throw new InvalidOperationException("JWT Secret not configured");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AppSettings:Jwt:Issuer"] ?? "MojiAPI",
            ValidAudience = builder.Configuration["AppSettings:Jwt:Audience"] ?? "MojiClient",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSecret))
        };
    });

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

var app = builder.Build();

// Initialize database and create SQLite offline database
using (var scope = app.Services.CreateScope())
{
    try
    {
        var offlineDb = scope.ServiceProvider.GetRequiredService<OfflineDbContext>();
        await offlineDb.Database.EnsureCreatedAsync();
        Console.WriteLine("SQLite offline database initialized successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error initializing offline database: {ex.Message}");
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Moji API V1");
        c.RoutePrefix = "";
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Moji API V1");
        c.RoutePrefix = "";
    });
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllers();



app.Run();