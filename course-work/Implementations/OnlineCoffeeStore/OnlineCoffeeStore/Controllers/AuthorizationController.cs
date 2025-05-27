using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeStore.DTO;
using OnlineCoffeeStore.Services;

namespace OnlineCoffeeStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public AuthorizationController(IJwtAuthenticationManager jwtAuthenticationManager) 
        { 
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var token = _jwtAuthenticationManager.Authenticate(request.Email, request.Password);

            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(new AuthenticationResponse(token));
        }

        //[HttpGet("logout")]
        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Remove("token");

        //    return RedirectToAction("Login", "Home");
        //}
    }
}
