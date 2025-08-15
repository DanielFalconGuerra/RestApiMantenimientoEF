using RestApiMantenimientoEF.Interfaces;
using RestApiMantenimientoEF.Modelos.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using RestApiMantenimientoEF.Repositories;

namespace RestApiMantenimientoEF.Security
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public Task<bool> IsValidUser(LoginDTO login)
        {
            // **Lógica de validación del usuario**
            // En una aplicación real, aquí buscarías el usuario en una base de datos
            // y compararías la contraseña cifrada.
            // Por ahora, usamos credenciales fijas para el ejemplo.
            var isValid = login.NoColaborador == "4452" && login.key == "admin123";
            return Task.FromResult(isValid);
        }

        public async Task<string> GenerateJwtToken(string username)
        {
            var secretKey = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Obtener el idAreaS del usuario
            int idAreaS = await _userRepository.GetIdAreaSByIdNoColaborador(int.Parse(username));

            // Opcional: Agrega "claims" para incluir información del usuario en el token.
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim("idAreaS", idAreaS.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30), // El token expira en 30 minutos
                signingCredentials: credentials);

            //return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
