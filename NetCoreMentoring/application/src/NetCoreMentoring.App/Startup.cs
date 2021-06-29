using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreMentoring.App.Extensions;
using NetCoreMentoring.App.Infrastructure;
using NetCoreMentoring.App.Models;
using NetCoreMentoring.Core.Extensions;
using Microsoft.OpenApi.Models;

namespace NetCoreMentoring.App
{
    public class Startup
    {
        private readonly string _contentRootPath;
        private const string DbConnectionStringName = "MyConnection";
        private const string IdentityDbConnectionStringName = "Identity";

        public Startup(IConfiguration configuration, IWebHostEnvironment host)
        {
            Configuration = configuration;
            _contentRootPath = host.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApp(Configuration, IdentityDbConnectionStringName, _contentRootPath);
            services.AddCore(Configuration.GetConnectionString(DbConnectionStringName), _contentRootPath);
            services.AddApiVersioning();

            services.AddAutoMapper(
                typeof(App.Mapping.MappingProfile).Assembly,
                typeof(Core.Mapping.MappingProfile).Assembly);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(ActionInvocationLoggingFilter));
            });
        }

        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                await next.Invoke();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMiddleware<ImageCacheMiddleware>(
                Configuration,
                new CachingOptions
                {
                    CachedCodePath = new Dictionary<string, string>()
                    {
                        {"/Category/GetPicture", "categoryId"}
                    }
                });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(name: "images",
                //    pattern: "images/{categoryId}",
                //    defaults: new { controller = "Category", action = "GetPicture"});
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}