using FBank.Application.Interfaces;
using FBank.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FBank.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool sqlServerTest = false)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IAgencyRepository, AgencyRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            if (sqlServerTest is false)
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
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            connectionString = connectionString.Replace("DataBase=fbank", "DataBase=fbank-teste");

            services.AddDbContext<DataBaseContext>(options => {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
