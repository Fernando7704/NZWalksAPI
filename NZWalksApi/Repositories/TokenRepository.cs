﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalksApi.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public readonly IConfiguration _configuration;
        public TokenRepository(IConfiguration configuration)
        {
            this._configuration = configuration;       
        }
        public string CreateJWToken(IdentityUser user, List<string> roles)
        {
            //Create claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in roles) {
            claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credenciale = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(                
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credenciale
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
