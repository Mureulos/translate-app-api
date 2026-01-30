using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using translate_app.Domain.Entities;

namespace translate_app.Infrastructure.Services
{
    public sealed class TokenService(IConfiguration configuration)
    {
        public string Create(User user)
        {
            string stringKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured.");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(stringKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new Dictionary<string, object>
            {
                { JwtRegisteredClaimNames.Sub, user.Id.ToString() },
                { JwtRegisteredClaimNames.Email, user.Email },
                { "email_verified", user.Email },
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]    
            };

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(tokenDescriptor);
            string token = handler.WriteToken(securityToken);

            return token;
        }
    }
}
