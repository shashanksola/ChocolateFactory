using ChocolateFactory.Models;
using ChocolateFactory.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChocolateFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsersByRoleAsync([FromBody] UserRole role)
        {
             var users = await _service.GetUsersByUserRoleAsync(role);
            return users;
        }
    }
}
