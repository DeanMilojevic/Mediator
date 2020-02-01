using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Mediator.AspNetCore.Example.Queries
{
    public class SampleQueryHandler : IQueryHandler<SampleQuery, SampleQueryResult>
    {
        private readonly ILogger<SampleQueryHandler> _logger;

        public SampleQueryHandler(ILogger<SampleQueryHandler> logger)
        {
            _logger = logger;
        }

        public async Task<SampleQueryResult> Handle(SampleQuery query)
        {
            _logger.LogInformation(query.Value);

            await Task.Delay(100);

            return new SampleQueryResult { Value = "Result from the Sample Query Handler" };
        }
    }
}
