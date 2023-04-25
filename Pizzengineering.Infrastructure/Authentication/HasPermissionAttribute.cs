using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pizzengineering.Domain.Entities;

namespace Pizzengineering.Infrastructure.Authentication;

public class HasPermissionAttribute : AuthorizeAttribute
{
	public HasPermissionAttribute(Permission permission)
		: base(policy: permission.ToString()!)
	{

	}
}