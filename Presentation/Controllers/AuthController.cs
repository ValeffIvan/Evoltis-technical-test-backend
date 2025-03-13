using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var resultado = await _authService.LoginAsync(loginDto);
            if (resultado == null)
                return Unauthorized("Credenciales inválidas");
            return Ok(resultado);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto loginDto)
        {
            var resultado = await _authService.RegisterAsync(loginDto);
            if (resultado == null)
                return BadRequest("No se pudo registrar el usuario");

            return Ok(resultado);
        }
    }
}
