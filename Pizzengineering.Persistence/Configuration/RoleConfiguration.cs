using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Persistence.Constants;

namespace Pizzengineering.Persistence.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.ToTable(TableNames.Roles);

		builder.HasKey(e => e.Id);

		builder.HasMany(e => e.Permissions)
			.WithMany()
			.UsingEntity<RolePermission>();

		builder.HasMany(x => x.Users)
			.WithMany(x => x.Roles);

		builder.HasData(Role.GetValues());
	}
}