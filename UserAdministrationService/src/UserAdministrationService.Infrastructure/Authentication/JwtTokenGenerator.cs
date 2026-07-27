using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.UserAggregate;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace UserAdministrationService.Infrastructure.Authentication;

internal class JwtTokenGenerator(IOptions<JwtOptions> jwtOptions) : IJwtTokenGenerator
{
	private readonly JwtOptions _jwtOptions = jwtOptions.Value;

	public string GenerateToken(User user)
	{
		SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
		SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

		Claim[] claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
			new Claim(JwtRegisteredClaimNames.Email, user.Email),
			new Claim("id", user.Id.ToString())
		};

		JwtSecurityToken token = new(
			_jwtOptions.Issuer,
			_jwtOptions.Audience,
			claims,
			expires: DateTime.UtcNow.AddMinutes(_jwtOptions.TokenExpirationInMinutes),
			signingCredentials: credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
