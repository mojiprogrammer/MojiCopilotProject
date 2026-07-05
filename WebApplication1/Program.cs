using DanaCopilot.Application.Modules.Configuration.Interfaces;
using DanaCopilot.Application.Modules.Configuration.Services;
using DanaCopilot.Application.Modules.Core.Interfaces;
using DanaCopilot.Application.Modules.Core.Services;
using DanaCopilot.Application.Modules.Oee.Interfaces;
using DanaCopilot.Application.Modules.Oee.Services;
using DanaCopilot.Application.Modules.RunTime.Interfaces;
using DanaCopilot.Application.Modules.RunTime.Services;
using DanaCopilot.Infrastructure.Connection;
using DanaCopilot.Infrastructure.DataAccess.Implements;
using DanaCopilot.Infrastructure.DataAccess.Interfaces;
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
using Moji.Services.Services;
using System.Text;
using TestSystem.Application.Modules.Configuration.Line.Services;

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

// Register Offline DbContext (SQLite) - For prediction module
var offlineConnectionString = builder.Configuration.GetConnectionString("OfflineConnection") ?? "Data Source=offline_prediction.db";
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
builder.Services.AddSingleton<IDbConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<ILineDataAccess, LineDataAccess>();
builder.Services.AddScoped<ILineApplicationService, LineApplicationService>();
builder.Services.AddScoped<IProductCategoryDataAccess, ProductCategoryDataAccess>();
builder.Services.AddScoped<IProductCategoryApplicationService, ProductCategoryApplicationService>();
builder.Services.AddScoped<IProductDataAccess, ProductDataAccess>();
builder.Services.AddScoped<IProductApplicationService, ProductApplicationService>();
builder.Services.AddScoped<IPLCTypeDataAccess, PLCTypeDataAccess>();
builder.Services.AddScoped<IPLCTypeApplicationService, PLCTypeApplicationService>();
builder.Services.AddScoped<IPLCConfigurationDefinitionDataAccess, PLCConfigurationDefinitionDataAccess>();
builder.Services.AddScoped<IPLCConfigurationDefinitionApplicationService, PLCConfigurationDefinitionApplicationService>();
builder.Services.AddScoped<IPLCConfigurationDataAccess, PLCConfigurationDataAccess>();
builder.Services.AddScoped<IPLCConfigurationApplicationService, PLCConfigurationApplicationService>();
builder.Services.AddScoped<IStationDataAccess, StationDataAccess>();
builder.Services.AddScoped<IStationApplicationService, StationApplicationService>();
builder.Services.AddScoped<IStationPLCDataAccess, StationPLCDataAccess>();
builder.Services.AddScoped<IStationPLCApplicationService, StationPLCApplicationService>();
builder.Services.AddScoped<IParameterDataAccess, ParameterDataAccess>();
builder.Services.AddScoped<IParameterApplicationService, ParameterApplicationService>();
builder.Services.AddScoped<IParameterMappingDataAccess, ParameterMappingDataAccess>();
builder.Services.AddScoped<IParameterMappingApplicationService, ParameterMappingApplicationService>();
builder.Services.AddScoped<IAlarmDefinitionDataAccess, AlarmDefinitionDataAccess>();
builder.Services.AddScoped<IAlarmDefinitionApplicationService, AlarmDefinitionApplicationService>();
builder.Services.AddScoped<IParameterValueDataAccess, ParameterValueDataAccess>();
builder.Services.AddScoped<IAlarmEventDataAccess, AlarmEventDataAccess>();
builder.Services.AddScoped<IAlarmEventApplicationService, AlarmEventApplicationService>();
builder.Services.AddScoped<IOeeCalculationService, OeeCalculationService>();
builder.Services.AddScoped<IOEECalculationService, OEECalculationService>();
builder.Services.AddScoped<IOEESnapshotDataAccess, OEESnapshotDataAccess>();
builder.Services.AddScoped<IOEESnapshotBuilderService, OEESnapshotBuilderService>();
builder.Services.AddScoped<IProductionExecutionDataAccess, ProductionExecutionDataAccess>();
builder.Services.AddScoped<IProductionExecutionApplicationService, ProductionExecutionApplicationService>();
builder.Services.AddScoped<IRejectReasonDataAccess, RejectReasonDataAccess>();
builder.Services.AddScoped<IRejectReasonApplicationService, RejectReasonApplicationService>();
builder.Services.AddScoped<IReworkReasonDataAccess, ReworkReasonDataAccess>();
builder.Services.AddScoped<IReworkReasonApplicationService, ReworkReasonApplicationService>();
builder.Services.AddScoped<IShiftDataAccess, ShiftDataAccess>();
builder.Services.AddScoped<IShiftScheduleDataAccess, ShiftScheduleDataAccess>();
builder.Services.AddScoped<IShiftCalendarDataAccess, ShiftCalendarDataAccess>();
builder.Services.AddScoped<IShiftApplicationService, ShiftApplicationService>();
builder.Services.AddScoped<IShiftScheduleApplicationService, ShiftScheduleApplicationService>();
builder.Services.AddScoped<IShiftCalendarApplicationService, ShiftCalendarApplicationService>();
builder.Services.AddScoped<IOEESnapshotDataAccess, OEESnapshotDataAccess>();


// Register background service for auto-training ML models
builder.Services.AddHttpClient();

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