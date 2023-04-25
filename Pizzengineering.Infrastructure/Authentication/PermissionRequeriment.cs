using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
