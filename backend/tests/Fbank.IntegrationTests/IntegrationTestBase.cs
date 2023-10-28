using FBank.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Fbank.IntegrationTests
{
    public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly DataBaseContext _dataBaseContext;
        private readonly IMediator _mediator;
        private readonly IServiceScope _serviceScope;
        private readonly IServiceProvider _serviceProvider;

        public WebApplicationFactory<Program> Factory => _factory;
        public DataBaseContext DataBaseContext => _dataBaseContext;
        public IMediator Mediator => _mediator;
        public IServiceProvider ServiceProvider => _serviceProvider;
        public IServiceScope ServiceScope => _serviceScope;

        public IntegrationTestBase(WebApplicationFactory<Program> factory)
        {
            Environment.SetEnvironmentVariable("FBANK_DB_NAME", "true");

            _factory = factory;
            _serviceScope = factory.Services.GetService<IServiceScopeFactory>()!.CreateScope()!;
            _dataBaseContext = _serviceScope.ServiceProvider.GetRequiredService<DataBaseContext>()!;
            _mediator = _serviceScope.ServiceProvider.GetService<IMediator>()!;

            
            _dataBaseContext.Database.EnsureCreated();
        }
        public void Dispose()
        {
            _dataBaseContext.Database.EnsureDeleted();
            _dataBaseContext.Dispose();
            _serviceScope.Dispose();
        }
    }
}
