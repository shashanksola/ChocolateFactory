using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChocolateFactory.Services;
using ChocolateFactory.Models;
using System.ComponentModel.DataAnnotations;

namespace ChocolateFactory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest loginRequest)
        {
            var user = await _authService.AuthenticateUserAsync(loginRequest.Username, loginRequest.Password);
            if (user == null) return Unauthorized("Invalid credentials");

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest registerRequest)
        {
            if (registerRequest.Role.ToString() == "FactoryManager")
            {
                return BadRequest("Cannot register users with FactoryManager role through this endpoint");
            }

            var newUser = new User
            {
                Username = registerRequest.Username,
                PasswordHash = registerRequest.Password, // Password will be hashed in AuthService
                Email = registerRequest.Email,
                Role = registerRequest.Role
            };

            var success = await _authService.RegisterUserAsync(newUser);

            if (!success) return BadRequest("Failed to register user");

            return Ok("User registered successfully");
        }

        [HttpPost("registerAdmin")]
        [Authorize(Roles = "FactoryManager")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserRegisterRequest registerRequest)
        {
            if (registerRequest.Role.ToString() != "Admin")
            {
                return BadRequest("This endpoint is only for registering Admin users");
            }

            var newUser = new User
            {
                Username = registerRequest.Username,
                PasswordHash = registerRequest.Password, // Password will be hashed in AuthService
                Email = registerRequest.Email,
                Role = registerRequest.Role
            };

            var success = await _authService.RegisterUserAsync(newUser);

            if (!success) return BadRequest("Failed to register Admin user");

            return Ok("Admin user registered successfully");
        }
    }


    public class UserRegisterRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        [EmailAddress]
        public required string Email {  get; set; }
        public required UserRole Role { get; set; }
    }
    public class UserLoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

}