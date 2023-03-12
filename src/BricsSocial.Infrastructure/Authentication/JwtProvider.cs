using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public string Generate(UserInfo userInfo)
        {
            var loginTime = DateTime.UtcNow;


            var _options = new IdentityOptions();
            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Email, userInfo.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.AuthTime, loginTime.ToString()),
                new(_options.ClaimsIdentity.UserIdClaimType, ""),
                new(_options.ClaimsIdentity.RoleClaimType, ""),

            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
                SecurityAlgorithms.HmacSha256Signature
                );

            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                null,
                loginTime.AddHours(1),
                signingCredentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenValue = tokenHandler.WriteToken(token);

            return tokenValue;
        }
    }
}
