using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace DanaCopilot.Auth
{
    public static class Extensions
    {
        public static void AddJwt(IServiceCollection services, IConfiguration configuration)
        {
            var options = new JwtOptions();
            var section = configuration.GetSection("jwtMicroService");
            section.Bind(options);
            services.Configure<JwtOptions>(configuration.GetSection("jwtMicroService"));
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddAuthentication().AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidIssuer = options.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),

                };
            }
            );
        }
    }
}
