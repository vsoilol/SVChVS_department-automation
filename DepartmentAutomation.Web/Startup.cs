using System.Collections.Generic;
using System.Net;
using DepartmentAutomation.Application;
using DepartmentAutomation.Application.Common.Options;
using DepartmentAutomation.Infrastructure;
using DepartmentAutomation.Web.Config;
using DepartmentAutomation.Web.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DepartmentAutomation.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCorsOrigin(services);
            
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.SetupSwagger();
            services.SetupJwtTokens(Configuration);

            services.AddControllers(options =>
                    {
                        options.Filters.Add<ApiExceptionFilterAttribute>();
                    })
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);
        }
        
        private void ConfigureCorsOrigin(IServiceCollection services)
        {
            var corsOrigins = new List<string>
            {
                "https://department-automation-angular.web.app", 
                "http://localhost:4200",
                "https://department-automation-angular.firebaseapp.com",
                "http://192.168.56.1:8080",
            };
            
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder =>
                    {
                        builder
                            .AllowCredentials()
                            .WithOrigins(corsOrigins.ToArray())
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
            });
            
            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}");
            });
        }
    }
}