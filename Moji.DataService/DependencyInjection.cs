using Microsoft.Extensions.DependencyInjection;
using Moji.DataService.Repositories.Interfaces;
using Moji.DataService.Repositories.ModelRepositories;

namespace Moji.DataService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<IUserRepositoryDataService, UserRepositoryDataService>();

            return services;
        }
    }
}
