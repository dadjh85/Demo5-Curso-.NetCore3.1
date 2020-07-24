using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Demo5.Infrastructure
{
    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder UseEndPointWithHealthCheck(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecksUI(setup =>
                {
                    setup.UIPath = "/healthcheckservices";
                    setup.AddCustomStylesheet("Infrastructure/CustomStylesheet/dotnet.css");
                }).WithDisplayName("healt checks services");
                
                endpoints.MapControllers();
            });

            return app;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso API-REST .NET Core 3.1 - Demo4");
                });
            }

            return app;
        }
    }
}
