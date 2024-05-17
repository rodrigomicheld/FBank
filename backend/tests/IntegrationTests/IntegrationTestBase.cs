using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public class IntegrationTestBase : IDisposable
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IServiceScope _serviceScope;
        private readonly IServiceProvider _serviceProvider;
        protected readonly IServiceCollection _servicesCollection;
        protected readonly IMediator _mediator;

        public IMediator Mediator => _mediator;
        public DataBaseContext DataBaseContext => _dataBaseContext;
        public IServiceScope ServiceScope => _serviceScope;
        public IServiceProvider ServiceProvider => _serviceProvider;


        public IntegrationTestBase()
        {
            _servicesCollection = TestServiceCollectionFactory.BuildIntegrationTestInfrastructure();

            _serviceProvider = GetServiceProvider(_servicesCollection);

            _serviceScope = _serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope()!;
            _dataBaseContext = _serviceScope.ServiceProvider.GetRequiredService<DataBaseContext>()!;
            _mediator = _serviceScope.ServiceProvider.GetService<IMediator>()!;

            _dataBaseContext.Database.EnsureDeleted();
            _dataBaseContext.Database.EnsureCreated();
        }

        public static IServiceProvider GetServiceProvider(IServiceCollection serviceCollection)
        {
            var defaultServiceProviderFactory = new DefaultServiceProviderFactory(new ServiceProviderOptions());
            return defaultServiceProviderFactory.CreateServiceProvider(serviceCollection);
        }

        public void Dispose()
        {
            _dataBaseContext.Dispose();
            _serviceScope.Dispose();
        }
    }
}
