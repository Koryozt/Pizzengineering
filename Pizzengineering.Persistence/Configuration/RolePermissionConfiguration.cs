using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.Enumerators;

namespace Pizzengineering.Persistence.Configuration;

public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
	public void Configure(EntityTypeBuilder<RolePermission> builder)
	{
		builder.HasKey(x => new { x.RoleId, x.PermissionId });

		builder.HasData(
			Create(Role.Registered, Permissions.Read));
	}

	private static RolePermission Create(Role role, Permissions permission)
	{
		return new RolePermission
		{
			RoleId = role.Id,
			PermissionId = (int)permission
		};
	}
}