using ImperiumLogistics.Infrastructure.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Implementation
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string GenerateToken(string username, Guid? userId, string role)
        {
            var issuer = configuration.GetSection("JwtSettings:Issuer").Value;
            var audience = configuration.GetSection("JwtSettings:Audience").Value;
            var secretKey = configuration.GetSection("JwtSettings:SecretKey").Value;
            var expirtyMinutes = configuration.GetSection("JwtSettings:ExpiryMinutes").Value;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha512
                );


            var securityToken = new JwtSecurityToken
                (
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToInt32(expirtyMinutes)),
                    signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public Dictionary<string, string>? ValidateToken(string token)
        {
            if(string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            var response = new Dictionary<string, string>();
            SecurityToken validatedToken;
            var issuer = configuration.GetSection("JwtSettings:Issuer").Value;
            var audience = configuration.GetSection("JwtSettings:Audience").Value;
            var secretKey = configuration.GetSection("JwtSettings:SecretKey").Value;
            var expirtyMinutes = configuration.GetSection("JwtSettings:ExpiryMinutes").Value;

            var tokenValidationParams = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            var securityToken = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParams, out validatedToken);
            if (validatedToken is not JwtSecurityToken jwtSecurityToken)
            {
                return null; 
            }

            string name = securityToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value ?? string.Empty;
            string userID = securityToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value ?? string.Empty;

            response.Add(JwtRegisteredClaimNames.Name, name);
            response.Add(JwtRegisteredClaimNames.Jti, userID);

            return response;
        }
    }
}
