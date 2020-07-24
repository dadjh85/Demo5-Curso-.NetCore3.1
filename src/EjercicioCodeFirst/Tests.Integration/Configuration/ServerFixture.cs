using Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Integration.Configuration
{
    public class ServerFixture
    {
        private static Checkpoint Checkpoint = new Checkpoint();

        public TestServer TestServer { get; private set; }

        private static IConfiguration Configuration;

        public const string EjercicioCodeFirstServerFixture = "EjercicioCodeFirstServerFixture";

        public ServerFixture()
        {
            IWebHostBuilder hostBuilder = new WebHostBuilder().ConfigureAppConfiguration((builder, config) =>
            {
                string publishPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                config.SetBasePath(publishPath)
                      .AddJsonFile("appsettings.json")
                      .AddJsonFile($"appsettings.{builder.HostingEnvironment.EnvironmentName}.json");

                Configuration = config.Build();

            }).UseEnvironment("Test")
            .UseStartup<TestStartup>();

            TestServer = new TestServer(hostBuilder);
            TestServer.Host.MigrateDbContext<DemoContext>((ctx, sp) => { });

            Checkpoint.TablesToIgnore = new[] { "__EFMigrationsHistory" };
        }

        public static void ResetDatabase()
        {
            Checkpoint.Reset(Configuration.GetConnectionString("Default")).Wait();
        }


        public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using var scope = TestServer.Host.Services.GetService<IServiceScopeFactory>().CreateScope();
            await action(scope.ServiceProvider);
        }

        public async Task ExecuteDbContextAsync(Func<DemoContext, Task> action)
        {
            await ExecuteScopeAsync(sp => action(sp.GetService<DemoContext>()));
        }
    }

    [CollectionDefinition(ServerFixture.EjercicioCodeFirstServerFixture)]
    public class ServerFixtureCollection : ICollectionFixture<ServerFixture>
    {

    }
}
