using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Security;
using VirtualHoftalon_Server.Models.Security.Dto;
using VirtualHoftalon_Server.Repositories.Interfaces;

namespace VirtualHoftalon_Server.Security;

public class TokenService
{

    public static TokenResponseDTO GenerateToken(Login login)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(Settings.Secret);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, login.Username),
                new Claim(ClaimTypes.Role, login.Role.ToString())
                
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            Issuer = Settings.Issuer, Audience = Settings.Audience
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return new TokenResponseDTO(tokenHandler.WriteToken(token), tokenDescriptor.Expires, GenerateRefreshToken());
    }

    public static string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}