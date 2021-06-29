using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCoreMentoring.Core.DataContext;
using NetCoreMentoring.Core.Services;
using NetCoreMentoring.Core.Services.Contracts;

namespace NetCoreMentoring.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(
            this IServiceCollection services,
            string dbConnectionString,
            string rootPath)
        {
            if (dbConnectionString.Contains("%CONTENTROOTPATH%"))
            {
                dbConnectionString = dbConnectionString.Replace("%CONTENTROOTPATH%", rootPath);
            }
            services.AddDbContext<NorthwindContext>(options =>
            {
                options.UseSqlServer(dbConnectionString);
            });

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}