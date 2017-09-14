using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WEB.ViewModels.Account;

namespace WEB.Providers
{
    public interface ITokenProvider
    {
        String GenerateToken(LoginViewModel loginViewModel);
    }

    public class TokenProvider : ITokenProvider
    {
        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            var secretKey = _configuration.GetValue<String>("secretKey");
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        }

        public String GenerateToken(LoginViewModel loginViewModel)
        {

            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, loginViewModel.Email),
                new Claim(JwtRegisteredClaimNames.Email, loginViewModel.Email)
            };

            var token = new JwtSecurityToken(
                issuer: "StoreAggregator Server",
                audience: "StoreAggregator Client",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(28),
                signingCredentials: new SigningCredentials(_key, SecurityAlgorithms.HmacSha256)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }

        private IConfiguration _configuration;

        private SymmetricSecurityKey _key;
    }
}
