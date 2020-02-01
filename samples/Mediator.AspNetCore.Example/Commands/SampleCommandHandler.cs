using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Mediator.AspNetCore.Example.Commands
{
    public class SampleCommandHandler : ICommandHandler<SampleCommand>
    {
        private readonly ILogger<SampleCommandHandler> _logger;

        public SampleCommandHandler(ILogger<SampleCommandHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SampleCommand command)
        {
            _logger.LogInformation(command.Value);

            return Task.CompletedTask;
        }
    }
}
