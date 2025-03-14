using Domain.DTOs;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<UserLoginDto> _loginValidator;
        private readonly IValidator<UserRegisterDto> _registerValidator;

        public AuthController(
            IAuthService authService,
            IValidator<UserLoginDto> loginValidator,
            IValidator<UserRegisterDto> registerValidator)
        {
            _authService = authService;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            // Validación manual con FluentValidation
            var validationResult = await _loginValidator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                // Extrae y retorna solo los mensajes de error
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(errorMessages);
            }

            try
            {
                var resultado = await _authService.LoginAsync(loginDto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            // Validación manual con FluentValidation
            var validationResult = await _registerValidator.ValidateAsync(registerDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(errorMessages);
            }

            try
            {
                var resultado = await _authService.RegisterAsync(registerDto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
