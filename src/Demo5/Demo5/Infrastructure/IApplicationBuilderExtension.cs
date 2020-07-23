using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

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
                
                //endpoints.MapControllers();
            });

            return app;
        }
    }
}
