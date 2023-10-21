using FBank.Application.Interfaces;
using FBank.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FBank.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IAgencyRepository, AgencyRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            var dbName = Environment.GetEnvironmentVariable("FBANK_DB_NAME") ?? "false";

            if (bool.Parse(dbName) is false)
                AddSqlServer(services, configuration);
            else
                AddSqlServerTest(services, configuration);

            return services;
        }

        private static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataBaseContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                });
        }

        private static void AddSqlServerTest(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataBaseContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("TestConnection"));
            });
        }
    }
}
