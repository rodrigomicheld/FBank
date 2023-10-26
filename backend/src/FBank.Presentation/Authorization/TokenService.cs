using FBank.Application.Interfaces;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FBank.Presentation.Authorization
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Client client)
        {
            var tokenHandle = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Secret"));
            var agency = client.Accounts.FirstOrDefault(x => x.Status == AccountStatus.Active).Number.ToString();
            var account = client.Accounts.FirstOrDefault(x => x.Status == AccountStatus.Active).Agency.Code.ToString();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Document", client.Document),
                    new Claim(ClaimTypes.Name, client.Name),
                    new Claim ("Account" , agency),
                    new Claim ("Agenccy" , account)
                    //new Claim(ClaimTypes.Role, client.Permission.ToString())
                }),

                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandle.CreateToken(tokenDescriptor);

            return tokenHandle.WriteToken(token);
        }
    }
}
