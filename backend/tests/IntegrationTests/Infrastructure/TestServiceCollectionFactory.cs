using Application;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Authorization;

namespace IntegrationTests
{
    public static class TestServiceCollectionFactory
    {
        public static IServiceCollection BuildIntegrationTestInfrastructure()
        {
            var services = new ServiceCollection();

            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();

            services.AddSingleton(configuration);

            services.AddAplication();
            services.AddInfrastructure(configuration, true);
            services.AddAuthorization(configuration);
            services.AddLogging();

            return services;
        }
    }
}
