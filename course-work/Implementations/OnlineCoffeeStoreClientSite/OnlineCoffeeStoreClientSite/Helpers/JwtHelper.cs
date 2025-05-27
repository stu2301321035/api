using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using OnlineCoffeeStoreClientSite.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineCoffeeStoreClientSite.Helpers
{
    public static class JwtHelper
    {
        public static int? GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Console.WriteLine("JWT Claims:");
            foreach (var claim in jwtToken.Claims)
            {
                Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
            }

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier || c.Type == "nameid");

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                Console.WriteLine($"Extracted User ID from JWT: {userId}");  // Добави лог

                return userId;
            }

            return null;
        }

        public static string? GetRoleFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role" || c.Type == ClaimTypes.Role);

            foreach (var claim in jwtToken.Claims)
            {
                Console.WriteLine($"CLAIM: {claim.Type} = {claim.Value}");
            }

            return roleClaim?.Value;
        }

    }
}
