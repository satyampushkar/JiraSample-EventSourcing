using JiraSample.Auth.Application.Common.Contracts.Authentication;
using JiraSample.Auth.Entities;
using JiraSample.Auth.Infrastructure.Authentication.Config;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JiraSample.Auth.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
