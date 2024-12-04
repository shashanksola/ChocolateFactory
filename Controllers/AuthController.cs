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
        [Authorize(Roles ="FactoryManager")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest registerRequest)
        {

            var newUser = new User
            {
                Username = registerRequest.Username,
                PasswordHash = registerRequest.Password, 
                Email = registerRequest.Email,
                Role = registerRequest.Role
            };

            var success = await _authService.RegisterUserAsync(newUser);

            if (!success) return BadRequest("Failed to register user");

            return Ok("User registered successfully");
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