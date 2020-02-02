using System;
using System.Threading.Tasks;

namespace Mediator.AspNetCore.DependencyInjection.Extensions
{
    internal sealed class ServiceProviderDispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Send(ICommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var genericType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());

            var commandHandler = (dynamic)_serviceProvider.GetService(genericType);

            EnsureHandlerExists(commandHandler, "The command handler is not registered.");

            await commandHandler.Handle((dynamic)command);
        }

        public async Task<T> Send<T>(IQuery<T> query)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var genericType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(T));

            var queryHandler = (dynamic)_serviceProvider.GetService(genericType);

            EnsureHandlerExists(queryHandler, "The query handler is not registered.");

            return await queryHandler.Handle((dynamic)query);
        }

        private void EnsureHandlerExists(dynamic handler, string errorMessage)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(errorMessage);
            }
        }
    }
}
