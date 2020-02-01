using System.Threading.Tasks;
using Mediator.AspNetCore.Example.Commands;
using Mediator.AspNetCore.Example.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mediator.AspNetCore.Example.Controllers
{
    [ApiController]
    [Route("api/run")]
    public class ExampleController : ControllerBase
    {
        private readonly ILogger<ExampleController> _logger;
        private readonly IDispatcher _dispatcher;

        public ExampleController(ILogger<ExampleController> logger, IDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var query = new SampleQuery
            {
                Value = "Value of the Sample Query"
            };

            var result = await _dispatcher.Send(query);

            _logger.LogInformation(result.Value);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync()
        {
            var command = new SampleCommand
            {
                Value = "Value of the Sample Command"
            };

            await _dispatcher.Send(command);

            return Ok();
        }
    }
}
