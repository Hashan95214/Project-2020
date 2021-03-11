using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using cakeworld.Models;
using Microsoft.Extensions.Configuration;


namespace cakeworld.Services.JWT_Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _config;
        private readonly OnlineDBContext _context;
       

        public JWTService(IConfiguration config, OnlineDBContext context)
        {
            _config = config;
            _context = context;
        }

        public string GenerateJWTtoken(Login user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Find User role
            var isSeller = _context.Sellers.FirstOrDefault(m => m.Email.ToLower() == user.Email.ToLower());
            var isBuyer = _context.Buyers.FirstOrDefault(m => m.Email.ToLower() == user.Email.ToLower());
            var isAdmin = _context.Admins.FirstOrDefault(m => m.Email.ToLower() == user.Email.ToLower());

            var currentUserRole = new object();
            var currentUserId = new object();
            var currentUserFirstName = new object();


            if (isSeller != null)
            {
                currentUserRole = isSeller.UserRole;
                currentUserId = isSeller.Id;
                currentUserFirstName = isSeller.FirstName;
              


            }
            else if (isAdmin != null)
            {
                currentUserRole = isAdmin.UserRole;
                currentUserId = isAdmin.Id;
                currentUserFirstName = isAdmin.FirstName;



            }
            else
            {
                currentUserRole = isBuyer.UserRole;
                currentUserId = isBuyer.Id;
                currentUserFirstName = isBuyer.FirstName;
            }
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                 new Claim("firstName", currentUserFirstName.ToString()),
                new Claim("id", currentUserId.ToString()),
                new Claim("role", currentUserRole.ToString()),
               
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
