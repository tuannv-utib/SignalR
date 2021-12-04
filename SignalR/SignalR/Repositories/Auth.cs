using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SignalR.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignalR.Repositories
{
    public class Auth
    {
        public string Authenticate(int user_id)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ReadConfig.AppSetting("Secret"));
            List<Claim> claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.NameIdentifier, user_id.ToString()));
            claim.Add(new Claim(ClaimTypes.Role, "AuthHub"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(ReadConfig.AppSetting("AuthExpiredTime"))), // Expires after xx minutes
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token_data = tokenHandler.CreateToken(tokenDescriptor);
            var token_str = tokenHandler.WriteToken(token_data);
            return token_str;
        }
    }
}
