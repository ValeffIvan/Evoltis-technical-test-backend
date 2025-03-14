using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public AuthService(ILoginRepository loginRepository, IMapper mapper, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _mapper = mapper;

            // Se extraen las configuraciones desde el appsettings.json o variables de entorno
            var secretKey = configuration["Jwt:SecretKey"];
            var expirationDays = int.Parse(configuration["Jwt:ExpirationDays"] ?? "2");
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];

            // Se inyecta la configuración en el servicio de token
            _tokenService = new TokenService(secretKey, expirationDays, issuer, audience);
        }

        public async Task<UserResponseDto> LoginAsync(UserLoginDto userLoginDto)
        {
            // 1. Buscar el usuario por email en la base de datos
            var user = await _loginRepository.GetUserByEmailAsync(userLoginDto.Email);

            if (user == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            // 2. Verificar si la contraseña es correcta
            if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password))
            {
                throw new Exception("Credenciales inválidas.");
            }

            // 3. Generar el token JWT
            var token = _tokenService.GenerateToken(user);

            // 4. Mapear la entidad a DTO de respuesta y asignar el token
            var responseDto = _mapper.Map<UserResponseDto>(user);
            responseDto.Token = token;

            return responseDto;
        }


        public async Task<UserResponseDto> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            // 1. Verificar si ya existe un usuario con el mismo Email
            var existingUser = await _loginRepository.GetUserByEmailAsync(userRegisterDto.Email);
            if (existingUser != null)
            {
                throw new Exception("El usuario ya existe.");
            }

            // 2. Hashear la contraseña
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password);

            // 3. Mapear el DTO a la entidad de dominio usando AutoMapper
            var user = _mapper.Map<User>(userRegisterDto);
            user.Password = passwordHash;

            // 4. Guardar el usuario en la base de datos
            var newUser = await _loginRepository.RegisterAsync(user);

            // 5. Generar token
            var token = _tokenService.GenerateToken(newUser);

            // 6. Mapear la entidad a DTO de respuesta y asignar el token
            var responseDto = _mapper.Map<UserResponseDto>(newUser);
            responseDto.Token = token;
            return responseDto;
        }
    }
}
