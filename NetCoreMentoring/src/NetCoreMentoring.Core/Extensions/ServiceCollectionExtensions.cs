using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreMentoring.Core.Services;
using NetCoreMentoring.Core.Services.Contracts;

namespace NetCoreMentoring.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}