using Microsoft.AspNetCore.Mvc;
using log_and_reg.Services;
using log_and_reg.Models;
using System.Threading.Tasks;

namespace log_and_reg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            if (string.IsNullOrWhiteSpace(request.user_name) || string.IsNullOrWhiteSpace(request.password))
            {
                return BadRequest(new {message =  "Username or password cannot be empty" });
            }

            var user = new User { user_name = request.user_name };
            await _userService.Register(user, request.password);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            if(string.IsNullOrWhiteSpace(request.user_name) || string.IsNullOrWhiteSpace(request.password))
            {
                return BadRequest(new { message = "Username or password cannot be empty" });
            }

            var user = await _userService.Login(request.user_name, request.password);
            if (user == null)
                return Unauthorized(new { message = "Username or password is incorrect" });

            return Ok(new { message = "User logged in successfully" });
        }
    }

    public class UserRegisterDto
    {
        public string user_name { get; set; }
        public string password { get; set; }
    }

    public class UserLoginDto
    {
        public string user_name { get; set; }
        public string password { get; set; }
    }
}
