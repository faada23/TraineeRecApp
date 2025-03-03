using System.Security.Claims;
using System.Text;
using Api.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    public string? GenerateToken(User user)
    {
        var claims = new List<Claim>{
            new Claim("Id",user.Id.ToString()),
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Fullname),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
        };

        var signCred = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey)),
            SecurityAlgorithms.HmacSha256
            );

        var expireTime = DateTime.UtcNow.Add(options.Value.Expires);

        var jwtToken = new JwtSecurityToken(
            claims : claims,
            signingCredentials : signCred,
            expires: expireTime
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}