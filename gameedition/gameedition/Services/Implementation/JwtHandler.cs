﻿using game.edition.api.DTO.HelperModels;
using game.edition.api.DTO.HelperModels.Jwt;
using game.edition.api.Extensions;
using game.edition.api.Models;
using game.edition.api.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace game.edition.api.Services.Implementation
{
    public class JwtHandler: IJwtHandler
    {
        AppConfiguration config = new AppConfiguration();
        public JwtResponse CreateToken(JwtCustomClaims model)
        {
            var now = DateTime.Now;
            var unixTimeSeconds = new DateTimeOffset(now).ToUnixTimeSeconds();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JWTSecret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                claims: new Claim[] {
                    new Claim(nameof(model.UserId), model.UserId.ToString()),
                    new Claim(nameof(model.UserName), model.UserName)
                },
                expires: DateTime.Now.AddHours(8),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            JwtResponse jwtresponse = new JwtResponse()
            {
                Token = tokenString,
                ExpiresAt = unixTimeSeconds
            };
            return jwtresponse;
        }
    }
}
