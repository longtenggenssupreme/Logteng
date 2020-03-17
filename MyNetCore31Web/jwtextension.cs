using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MyNetCore31Web
{
    public class Jwtextension
    {
        public string ProductJWT()
        {
            SigningCredentials signing = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mimamimamimamimamimamimamima")), SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer:"123", 
                audience:"456", 
                claims:new List<Claim> {new Claim(ClaimTypes.Sid,"123"),//id
                new Claim(ClaimTypes.WindowsAccountName,"account"),//account
                new Claim(ClaimTypes.Name,"name"),//name
                new Claim(ClaimTypes.UserData,"UserGroupName"),
                new Claim(JwtRegisteredClaimNames.Jti,"Jti"),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),}, 
                notBefore:DateTime.Now,
                expires : DateTime.Now.AddMinutes(10),
                signingCredentials: signing);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token = handler.WriteToken(jwt);
            return token;
        }
    }
}
