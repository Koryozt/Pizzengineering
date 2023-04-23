using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Persistence.Constants;

namespace Pizzengineering.Persistence.Configuration;

public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
	public void Configure(EntityTypeBuilder<Permission> builder)
	{
		builder.ToTable(TableNames.Permissions);

		builder.HasKey(e => e.Id);

		IEnumerable<Permission> permissions = Enum.GetValues<Pizzengineering.Domain.Enumerators.Permissions>()
			.Select(e => new Permission()
			{
				Id = (int)e,
				Name = e.ToString()
			});

		builder.HasData(permissions);
	}
}
