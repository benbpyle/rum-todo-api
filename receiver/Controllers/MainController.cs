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
        private readonly IHttpClientFactory _httpClientFactory;

        public ForecastController(ILogger<ForecastController> logger, IHttpClientFactory httpClientFactory
)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        [Route("/todos")]
        //[Trace(OperationName = "api.GetForecast", ResourceName = "Handler")]
        public async Task<ActionResult<ResponseBody>> Get()
        {
            this._logger.LogInformation("Request Received");

            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "http://api2:8080/users");
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                this._logger.LogInformation("Success calling /todos");
            }
            else
            {
                this._logger.LogError("Error calling /todos");
            }

            return await Task.Run(() =>
            {
                return Task.FromResult<ActionResult<ResponseBody>>(new ResponseBody("Hello"));
            });
        }
    }
}
