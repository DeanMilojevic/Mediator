using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Mediator.AspNetCore.DependencyInjection.Extensions
{
    internal sealed class ServiceProviderDispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ServiceProviderDispatcher> _logger;

        public ServiceProviderDispatcher(IServiceProvider serviceProvider, ILogger<ServiceProviderDispatcher> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Send(ICommand command)
        {
            var genericType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());

            var commandHandler = (dynamic)_serviceProvider.GetService(genericType);

            await commandHandler.Handle(command);
        }

        public async Task<T> Send<T>(IQuery<T> query)
        {
            var genericType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(T));

            var queryHandler = (dynamic)_serviceProvider.GetService(genericType);

            return (T) await queryHandler.Handle(query);
        }
    }
}
