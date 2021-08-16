using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.JwtFeatures.Options;
using Contracts;
using Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.JwtFeatures
{
    public class JwtHandler
    {
        private readonly ApplicationContext _appContext;
        private readonly JwtOptions _jwtOptions;

        public JwtHandler(
            IConfiguration configuration,
            ApplicationContext appContext)
        {
            _appContext = appContext;

            _jwtOptions = new JwtOptions();
            configuration.GetSection(JwtOptions.Section).Bind(_jwtOptions);
        }

        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey);
            var sercet = new SymmetricSecurityKey(key);

            return new SigningCredentials(sercet, SecurityAlgorithms.HmacSha256);
        }

        public List<Claim> GetClaims(Employee user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var role = user.Role;
            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));

            return claims;
        }

        public JwtSecurityToken GenerateTokenOptions(
            SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtOptions.ValidIssuer,
                audience: _jwtOptions.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtOptions.ExpiresInMinutes),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}
