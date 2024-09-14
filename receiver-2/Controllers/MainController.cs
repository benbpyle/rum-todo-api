using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Receiver2.Data;
using Receiver2.Models;

namespace Receiver2.Controllers
{

    public class ResponseBody(String name)
    {
        public String Name { get; set; } = name;
    }

    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _context;

        public UsersController(ILogger<UsersController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("/users/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            this._logger.LogInformation("(ID)={}, (User)={}", id, user);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet]
        [Route("/users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            this._logger.LogInformation("/users Request Received");
            return await this._context.Users.ToListAsync();
        }

    }
}
