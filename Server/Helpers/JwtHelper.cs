using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Helpers
{
    public class JwtHelper
    {
        private readonly JwtSettings _jwtSettings;

        public JwtHelper(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(User user, List<string> roles)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            foreach (string role in roles)
            {
                //var claim = new Claim(ClaimTypes.Role, role);
                claims.Add(new Claim(ClaimTypes.Role,role));
            }

            var token = new JwtSecurityToken
                (
                    claims : claims,
                    signingCredentials : credentials,
                    expires : DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                    issuer : _jwtSettings.Issuer,
                    audience : _jwtSettings.Audience
                );

            //string generateToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
