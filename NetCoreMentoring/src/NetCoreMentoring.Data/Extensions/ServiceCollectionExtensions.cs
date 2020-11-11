using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreMentoring.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddData(
            this IServiceCollection services,
            IConfiguration configuration,
            string dbConnectionStringName)
        {
            services.AddDbContext<NorthwindContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(dbConnectionStringName));
            });

            return services;
        }
    }
}