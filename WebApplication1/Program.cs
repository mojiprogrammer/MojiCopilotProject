using DanaCopilot.Application.Services;
using DanaCopilot.Infrastructure;
using DanaCopilot.Infrastructure.Interfaces;
using DanaCopilot.Infrastructure.Services.TestLine;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Moji.DataService;
using Moji.DataService.Repositories;
using Moji.DataService.Repositories.Interfaces;
using Moji.DataService.Repositories.ModelRepositories;
using Moji.Services.Helper;
using Moji.Services.Helper.Implement;
using Moji.Services.Interfaces;
using Moji.Services.Models;
using Moji.Services.Services;
using System.Text;
using Telegram.Bot;

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
        Title = "Dana API",
        Version = "v1",
        Description = "Dana Application API with JWT Authentication"
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

//TestLineServices
builder.Services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<LineRepository>();
builder.Services.AddScoped<LineService>();

// Register background service for auto-training ML models
builder.Services.AddHttpClient();

// 1. Register TelegramBotConfiguration directly
builder.Services.AddSingleton(sp =>
{
    var config = new DanaCopilot.Domain.Entities.TelegramBotConfiguration
    {
        BotToken = builder.Configuration["TelegramBot:BotToken"] ?? "",
        WebhookUrl = builder.Configuration["TelegramBot:WebhookUrl"] ?? "",
        BotUsername = builder.Configuration["TelegramBot:BotUsername"] ?? "",
        UseWebhook = bool.Parse(builder.Configuration["TelegramBot:UseWebhook"] ?? "true"),
        NotificationBatchSize = int.Parse(builder.Configuration["TelegramBot:NotificationBatchSize"] ?? "100"),
        MaxRetryAttempts = int.Parse(builder.Configuration["TelegramBot:MaxRetryAttempts"] ?? "3")
    };
    return config;
});

// 2. Register ITelegramBotClient as Singleton
builder.Services.AddSingleton<ITelegramBotClient>(sp =>
{
    var config = sp.GetRequiredService<DanaCopilot.Domain.Entities.TelegramBotConfiguration>();
    return new TelegramBotClient(config.BotToken);
});

// Configure prediction settings from appsettings.json
builder.Services.Configure<PredictionSettings>(builder.Configuration.GetSection("PredictionSettings"));

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
            ValidIssuer = builder.Configuration["AppSettings:Jwt:Issuer"] ?? "DanaAPI",
            ValidAudience = builder.Configuration["AppSettings:Jwt:Audience"] ?? "DanaClient",
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dana API V1");
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