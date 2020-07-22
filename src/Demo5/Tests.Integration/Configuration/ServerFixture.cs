using Database;
using Demo5;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Integration.Configuration
{
    public class ServerFixture
    {
        private static Checkpoint CheckPoint = new Checkpoint();

        public TestServer TestServer { get; private set; }

        private static IConfiguration Configuration;

        public const string Demo5ServerFixture = "Demo5ServerFixture";

        public ServerFixture()
        {
            IWebHostBuilder hostBuilder = new WebHostBuilder().ConfigureAppConfiguration((builder, config) =>
            {
                config.AddTestFilesConfiguration(builder.HostingEnvironment);

                Configuration = config.Build();
            })
            .UseEnvironment("Test")
            .UseStartup<TestStartup>();

            TestServer = new TestServer(hostBuilder);

            TestServer.Host.MigrateDbContext<DemoContext>((ctx, sp) => { });

            CheckPoint.TablesToIgnore = new[] { "__EFMigrationsHistory" };
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

        public static void ResetDatabase()
        {
            CheckPoint.Reset(Configuration.GetConnectionString("Default")).Wait();
        }
    }

    [CollectionDefinition(ServerFixture.Demo5ServerFixture)]
    public class ServerFixtureCollection : ICollectionFixture<ServerFixture>
    {
    }
}
