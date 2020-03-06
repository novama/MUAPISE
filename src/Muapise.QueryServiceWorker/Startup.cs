using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Muapise.QueryServiceWorker.Utils;
using NLog;

namespace Muapise.QueryServiceWorker
{
    public class Startup
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Logger.Debug("Services configuration started");
            services.Configure<AppSettings>(Configuration);
            var appSettings = Configuration.Get<AppSettings>();

            services.AddControllers();
            services.AddHttpClient("externalServices")
                .SetHandlerLifetime(TimeSpan.FromSeconds(30));

            //Adding Swagger
            if (appSettings.EnableSwagger)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(ApiInfo.CurrentApiVersion,
                        new OpenApiInfo
                        {
                            Title = AppInfo.AppName,
                            Version = ApiInfo.CurrentApiVersion,
                            Description = ApiInfo.ApiDescription
                        });
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var appSettings = Configuration.Get<AppSettings>();

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            if (appSettings.EnforceHttps)
            {
                Logger.Debug("Https redirection enforced.");
                // This setting enforce the use of the https route
                // (even when there is a valid http endpoint also
                // established into the appsettings.json)
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


            // Configuring Swagger UI
            if (appSettings.EnableSwagger)
            {
                app.UseSwagger(c => { c.RouteTemplate = ApiInfo.DefaultApiSwaggerRouteTemplate; });
                Logger.Debug("Api documentation json route path: {A}", ApiInfo.DefaultApiSwaggerEndPoint);
                // Swagger UI only avaible when environment is not production.
                if (!env.IsProduction())
                {
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/" + ApiInfo.DefaultApiSwaggerEndPoint,
                            ApiInfo.DefaultApiSwaggerName);
                        c.RoutePrefix = ApiInfo.ApiDocsUiSegmentName;
                        c.DocumentTitle = ApiInfo.DefaultApiSwaggerName;
                        //Removes Swagger Logo
                        c.HeadContent += "<style>.topbar-wrapper a {display: none !important;}</style>";
                    });
                    Logger.Debug("Api SwaggerUI active in non production environment. Route path: {A}",
                        ApiInfo.ApiDocsUiSegmentName);
                }
            }
        }
    }
}