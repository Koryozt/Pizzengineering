using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzengineering.Persistence.Constants;

public static class TableNames
{
	public const string Orders = "Orders";
	public const string Payments = "PaymentsInformation";
	public const string Permissions = "Permissions";
	public const string Pizzas = "Pizzas";
	public const string Roles = "Roles";
	public const string RolePermissions = "RolePermissionRelationship";
	public const string Users = "Users";
	public const string Outbox = "OutboxMessages";
}
