using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Pizzengineering.Infrastructure.Authentication.Setup;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
	private readonly string _sectionName = "Jwt";
	private readonly IConfiguration _configuration;

	public JwtOptionsSetup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void Configure(JwtOptions options)
	{
		_configuration.GetSection(_sectionName).Bind(options);
	}
}