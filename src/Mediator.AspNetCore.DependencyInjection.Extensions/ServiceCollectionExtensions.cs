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

        public static IServiceCollection AddCommandHandler<TCommand, Handler>(this IServiceCollection services)
            where Handler : class, ICommandHandler<TCommand>
            where TCommand : class, ICommand
        {
            services.AddSingleton<ICommandHandler<TCommand>, Handler>();

            return services;
        }

        public static IServiceCollection AddQueryHandler<TQuery, TResult, Handler>(this IServiceCollection services)
            where Handler : class, IQueryHandler<TQuery, TResult>
            where TQuery : class, IQuery<TResult>
        {
            services.AddSingleton<IQueryHandler<TQuery, TResult>, Handler>();

            return services;
        }
    }
}
