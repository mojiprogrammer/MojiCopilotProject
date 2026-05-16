using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Moji.DataService;
using Moji.DataService.Repositories.Interfaces;
using Moji.DataService.Repositories.ModelRepositories;
using Moji.Services.Interfaces;
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
        Description = "Moji Copilot Application API with JWT Authentication"
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
builder.Services.AddScoped<IUserRepositoryDataService, UserRepositoryDataService>();
builder.Services.AddScoped<IUserProfileRepositoryDataService, UserProfileRepositoryDataService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

builder.Services.AddHttpClient();

// Authentication - MOVE THIS BEFORE AddAuthentication
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

// Configure the HTTP request pipeline - ORDER MATTERS HERE!
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

// CRITICAL: Middleware order must be:
// 1. UseRouting
// 2. UseCors
// 3. UseAuthentication
// 4. UseAuthorization
// 5. UseEndpoints (implied by MapControllers)

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseRouting();  // <-- Move this BEFORE authentication/authorization
app.UseAuthentication();  // <-- Must be after UseRouting
app.UseAuthorization();   // <-- Must be after UseAuthentication
app.MapStaticAssets();
app.MapControllers();  // <-- This replaces UseEndpoints

app.Run();