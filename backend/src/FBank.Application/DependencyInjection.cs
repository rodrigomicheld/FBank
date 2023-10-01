using FBank.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FBank.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddMediatR(x=> x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper (typeof(BaseEntityToViewModelMapping));

            return services;
        }
    }
}
