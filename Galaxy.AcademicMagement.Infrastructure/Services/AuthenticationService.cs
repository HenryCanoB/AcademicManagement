using Galaxy.AcademicMagement.Application.Contracts.Services;
using Galaxy.AcademicMagement.Domain.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Galaxy.AcademicMagement.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User request, string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("Secret Key is required.");
            }

            var minutesExp = Convert.ToInt32(jwtSettings["ExpirationMinutes"]);
            var expirationDate = DateTime.UtcNow.AddMinutes(minutesExp);

            // Header
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // Payload - Claims
            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub, request.Id.ToString()),
                new (JwtRegisteredClaimNames.Email, request.Email),
                new (ClaimTypes.NameIdentifier, request.UserName),
                new (ClaimTypes.Name, request.FullName),
                new (ClaimTypes.Expiration, expirationDate.ToString("yyyy-MM-dd hh:mm:ss")),
                new (ClaimTypes.Role, role)
            };

            // Signature
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expirationDate,
                signingCredentials: new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
