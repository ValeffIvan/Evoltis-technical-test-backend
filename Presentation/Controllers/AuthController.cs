using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;

        public AuthController(IAuth authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto loginDto)
        {
            var resultado = await _authService.LoginAsync(loginDto.Email, loginDto.Password);
            if (resultado == null)
                return Unauthorized("Credenciales inválidas");

            // Aquí podrías generar y devolver un token JWT si lo requieres
            return Ok(resultado);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioRegisterDto loginDto)
        {
            var resultado = await _authService.Register(loginDto.Name, loginDto.Email, loginDto.Password);
            if (resultado == null)
                return BadRequest("No se pudo registrar el usuario");

            return Ok(resultado);
        }
    }
}
