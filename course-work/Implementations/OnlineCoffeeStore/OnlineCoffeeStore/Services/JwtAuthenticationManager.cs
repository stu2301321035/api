using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using OnlineCoffeeStore.Data;
using OnlineCoffeeStore.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineCoffeeStore.Services
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly string _tokenKey;
        private readonly ApplicationDbContext _context;

        public JwtAuthenticationManager(string tokenKey, ApplicationDbContext context) { 
            _tokenKey = tokenKey;
            _context = context;
        }

        public string? Authenticate(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password) ;

            if (user == null) 
            { 
                return null;    
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Email, user.Email!),
                    new (ClaimTypes.Role, ((AvailableRoles)user.Role).ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
