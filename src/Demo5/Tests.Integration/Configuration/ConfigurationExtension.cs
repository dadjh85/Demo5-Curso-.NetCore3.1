using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;

namespace Tests.Integration.Configuration
{
    public static class ConfigurationExtension
    {
        /// <summary>
        /// Load Files Configuration for testing
        /// </summary>
        /// <param name="config"></param>
        /// <param name="env"></param>
        /// <returns>a object IConfigurationBuilder</returns>
        public static IConfigurationBuilder AddTestFilesConfiguration(this IConfigurationBuilder config, IHostEnvironment env)
        {
            string publishPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            config.SetBasePath(publishPath)
                  .AddJsonFile("appsettings.json")
                  .AddJsonFile($"appsettings.{env.EnvironmentName}.json");

            return config;
        }
    }
}
