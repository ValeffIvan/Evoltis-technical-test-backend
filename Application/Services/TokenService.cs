using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

public class TokenService
{
    private readonly string _secretKey;
    private readonly int _expirationDays;
    private readonly string _issuer;
    private readonly string _audience;

    public TokenService(string secretKey, int expirationDays = 2, string issuer = null, string audience = null)
    {
        if (string.IsNullOrEmpty(secretKey))
            throw new ArgumentException("La clave secreta no puede ser nula o vacía.", nameof(secretKey));

        _secretKey = secretKey;
        _expirationDays = expirationDays;
        _issuer = issuer;
        _audience = audience;
    }

    public string GenerateToken(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        // Definir las reclamaciones a incluir en el token
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Name),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Usar la clave secreta directamente, sin hash
        var keyBytes = Encoding.UTF8.GetBytes(_secretKey);
        var key = new SymmetricSecurityKey(keyBytes);
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Configuración del token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(_expirationDays),
            SigningCredentials = creds,
            Issuer = _issuer,
            Audience = _audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
