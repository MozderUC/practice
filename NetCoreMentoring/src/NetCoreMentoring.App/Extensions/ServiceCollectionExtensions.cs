using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreMentoring.App.Infrastructure;

namespace NetCoreMentoring.App.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApp(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ActionInvocationLoggingAttribute>();

            return services;
        }
    }
}