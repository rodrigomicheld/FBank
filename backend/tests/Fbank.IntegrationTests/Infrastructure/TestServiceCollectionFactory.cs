using FBank.Application;
using FBank.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Fbank.IntegrationTests
{
    public static class TestServiceCollectionFactory
    {
        public static IServiceCollection BuildIntegrationTestInfrastructure()
        {
            var services = new ServiceCollection();
            var mockConfiguration = Substitute.For<IConfiguration>();

            services.AddSingleton(_ => mockConfiguration);
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            services.AddAplication();
            services.AddInfrastructure(configuration, true);
            services.AddLogging();

            return services;
        }
    }
}