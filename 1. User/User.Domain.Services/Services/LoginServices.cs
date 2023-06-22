using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Business.DTO;
using User.Domain.Business.DTO.Token;
using User.Domain.Interface.IServices;

namespace User.Domain.Services.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly IConfiguration _configuration;

        public LoginServices(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public TokenResponse CreateToken(UserResponseDTO user)
        {
            var claims = new[]
            {
                new Claim("Document", user.Documento.ToString()),
                new Claim("user", user.Correo),
                new Claim("IdUser", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Nombre + " " + user.Apellido),
                new Claim(ClaimTypes.Role, user.IdRol.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //TODO: Se deja el Token habilitado por 12 horas
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["AuthorizationServer:Authority"],
                _configuration["AuthorizationServer:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: credentials);

            var Tokenresults = new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
            };

            return new TokenResponse { Token = Tokenresults.Token, Expiration = Tokenresults.ExpirationLocal };
        }
    }
}
