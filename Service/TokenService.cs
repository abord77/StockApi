using LearningApi.Interfaces;
using LearningApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LearningApi.Service {
    public class TokenService : ITokenService { // comes from episode 23
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _Key;
        public TokenService(IConfiguration configuration) {
            _configuration = configuration;
            _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
        }

        public string CreateToken(AppUser user) { // this is where claims are put into a token
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            }; // claims we want

            var credentials = new SigningCredentials(_Key, SecurityAlgorithms.HmacSha512Signature); // encryption

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            }; // object representation of a token, .NET will handle the conversion

            var tokenHandler = new JwtSecurityTokenHandler(); // make a handler
            var token = tokenHandler.CreateToken(tokenDescriptor); // invoke the handler on the object representation

            return tokenHandler.WriteToken(token); // token is still an object (SecurityToken) and WriteToken is a method that converts to a string
        }
    }
}
