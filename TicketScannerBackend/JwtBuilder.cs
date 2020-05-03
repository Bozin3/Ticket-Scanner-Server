using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TicketScannerBackend.Models;

namespace TicketScannerBackend
{
    public static class JwtBuilder
    {
        public static string CreateJwtToken(this Clients client, IConfiguration configuration)
        {
            Claim[] claims ={
                new Claim(ClaimTypes.NameIdentifier,client.Id.ToString()),
                new Claim(ClaimTypes.Name,client.Username)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDesc = new SecurityTokenDescriptor();
            tokenDesc.Subject = new ClaimsIdentity(claims);
            tokenDesc.SigningCredentials = creds;
            tokenDesc.Expires = DateTime.Now.AddDays(1);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesc);

            return tokenHandler.WriteToken(token); ;
        }
    }
}