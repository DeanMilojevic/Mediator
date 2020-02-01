using Microsoft.Extensions.DependencyInjection;

namespace Mediator.AspNetCore.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDispatcher(this IServiceCollection services)
        {
            services.AddSingleton<IDispatcher, ServiceProviderDispatcher>();

            return services;
        }
    }
}
