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
            string dbConnectionStringName,
            string rootPath)
        {
            var connectionString = configuration.GetConnectionString(dbConnectionStringName);
            if (connectionString.Contains("%CONTENTROOTPATH%"))
            {
                connectionString = connectionString.Replace("%CONTENTROOTPATH%", rootPath);
            }
            services.AddDbContext<NorthwindContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}