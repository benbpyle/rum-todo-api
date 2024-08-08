using Microsoft.AspNetCore.Mvc;

namespace Receiver.Controllers
{

    public class ResponseBody(String name)
    {
        public String Name { get; set; } = name;
    }

    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly ILogger _logger;

        public ForecastController(ILogger<ForecastController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        //[Trace(OperationName = "api.GetForecast", ResourceName = "Handler")]
        [Route("/users")]
        public Task<ActionResult<ResponseBody>> Get()
        {
            this._logger.LogInformation("/todos Request Received");
            return Task.FromResult<ActionResult<ResponseBody>>(new ResponseBody("Hello"));
        }

    }
}
