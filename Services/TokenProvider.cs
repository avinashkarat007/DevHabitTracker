﻿using DevHabitTracker.DTOs.Auth;
using DevHabitTracker.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevHabitTracker.Services
{

    public class TokenProvider(IOptions<JwtAuthOptions> options)
    {
        private readonly JwtAuthOptions _jwtAuthOptions = options.Value;

        public AccessTokensDto Create(TokenRequest tokenRequest)
        {
            return new AccessTokensDto(GenerateAccessToken(tokenRequest), string.Empty);
        }

        private string GenerateAccessToken(TokenRequest tokenRequest)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthOptions.Key));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            List<Claim> claims = [
                new(JwtRegisteredClaimNames.Sub, tokenRequest.UserId),
                new(JwtRegisteredClaimNames.Email, tokenRequest.Email),
                ..tokenRequest.Roles.Select(role => new Claim(ClaimTypes.Role, role))
                ];


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtAuthOptions.ExpirationInMinutes),
                SigningCredentials = credentials,
                Issuer = _jwtAuthOptions.Issuer,
                Audience = _jwtAuthOptions.Audience,
            };


            var handler = new Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler();

            var accessToken = handler.CreateToken(tokenDescriptor);

            return accessToken;
        }

        private string GenerateRefreshToken()
        {
            return string.Empty;
        }
    }
}
