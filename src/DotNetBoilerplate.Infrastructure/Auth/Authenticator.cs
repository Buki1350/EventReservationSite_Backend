using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetBoilerplate.Application.Security;
using DotNetBoilerplate.Application.Security.Responses;
using DotNetBoilerplate.Shared.Abstractions.Time;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DotNetBoilerplate.Infrastructure.Auth;

public class Authenticator : IAuthenticator
{
    private readonly string _audience;
    private readonly IClock _clock;
    private readonly TimeSpan _expiry;
    private readonly string _issuer;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
    private readonly SigningCredentials _signingCredentials;

    public Authenticator(IOptions<AuthOptions> options, IClock clock)
    {
        _clock = clock;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _expiry = options.Value.Expiry ?? TimeSpan.FromHours(1);
        _signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigningKey)),
                SecurityAlgorithms.HmacSha256);
    }

    public JwtResponse CreateToken(Guid userId, string role)
    {
        var now = _clock.Now();
        var expires = now.Add(_expiry);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(ClaimTypes.Role, role)
        };

        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);

        var accessToken = _jwtSecurityTokenHandler.WriteToken(jwt);

        return new JwtResponse
        {
            AccessToken = accessToken
        };
    }
}