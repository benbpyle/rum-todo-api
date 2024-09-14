using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Receiver.Data;
using Receiver.Models;

namespace Receiver.Controllers
{
    public class UserBody
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime Timestamp { get; set; }

    }

    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public TodosController(ILogger<TodosController> logger, AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _context = context;
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        [Route("/todos")]
        //[Trace(OperationName = "api.GetForecast", ResourceName = "Handler")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            this._logger.LogInformation("Request Received");
            var todos = await this._context.Todos.ToListAsync();
            var httpClient = _httpClientFactory.CreateClient();

            foreach (var t in todos)
            {
                this._logger.LogInformation("Making a request for: " + t.UserId);
                /*var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://api2:8080/users/" + t.UserId);*/
                /*var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);*/
                var body = await httpClient.GetFromJsonAsync<UserBody>("http://api2:8080/users/" + t.UserId,
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                if (body != null)
                {
                    t.Username = body.Username;
                }
                else
                {
                    t.Username = "Unassigned";
                }
            }

            return todos;
        }
    }
}
