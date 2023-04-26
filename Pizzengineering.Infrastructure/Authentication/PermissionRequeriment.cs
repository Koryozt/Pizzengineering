using Microsoft.AspNetCore.Authorization;

namespace Pizzengineering.Infrastructure.Authentication;

public class PermissionRequeriment : IAuthorizationRequirement
{
	public string Permission { get; set; }

	public PermissionRequeriment(string permission)
	{
		Permission = permission;
	}
}
