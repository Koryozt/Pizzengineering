using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pizzengineering.Application.Abstractions;
using Pizzengineering.Domain.Entities;

namespace Pizzengineering.Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
	private readonly JwtOptions _options;

	public JwtProvider(IOptions<JwtOptions> options)
	{
		_options = options.Value;
	}

	public string Generate(User user)
	{
		var claims = new Claim[]
		{
			new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new(JwtRegisteredClaimNames.Email, user.Email.Value)
		};

		var signinCredentials = new SigningCredentials(
			new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(_options.SecretKey)),
			SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			_options.Issuer,
			_options.Audience,
			claims,
			null,
			DateTime.UtcNow.AddHours(1),
			signinCredentials);

		string tokenValue = new JwtSecurityTokenHandler()
			.WriteToken(token);

		return tokenValue;
	}
}