using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStore.src.Entity;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.src.Utils
{
    public class TokenUtils
    {
        private readonly IConfiguration _config;

        public TokenUtils(IConfiguration config)
        {
            _config = config;
        }

        public string GnerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()!),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value!)
            );
            var signinCredentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256Signature
            );
            var desc = new SecurityTokenDescriptor
            {
                Issuer = _config.GetSection("Jwt:Issuer").Value,
                Audience = _config.GetSection("Jwt:Audience").Value,
                Expires = DateTime.Now.AddHours(1.5),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signinCredentials,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(desc);
            return tokenHandler.WriteToken(token);
        }
    }
}
