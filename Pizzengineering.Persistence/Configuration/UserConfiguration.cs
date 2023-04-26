using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.User;
using Pizzengineering.Persistence.Constants;

namespace Pizzengineering.Persistence.Configuration;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable(TableNames.Users);

		builder.HasKey(e => e.Id);

		builder.HasIndex(x => x.Email)
				.IsUnique();

		builder.Property(e => e.Firstname)
			.HasConversion(x => x.Value, v => Name.Create(v).Value);

		builder.Property(e => e.Lastname)
			.HasConversion(x => x.Value, v => Name.Create(v).Value);

		builder.Property(e => e.Email)
			.HasConversion(x => x.Value, v => Email.Create(v).Value);

		builder.Property(e => e.Password)
			.HasConversion(x => x.Value, v => Password.Create(v).Value);

		builder.HasMany(e => e.OrdersMade)
				.WithOne(e => e.User)
				.HasForeignKey(e => e.UserId);

		builder.HasOne(e => e.PaymentInformation)
				.WithOne(e => e.User)
				.HasForeignKey<PaymentInformation>(e => e.UserId);
	}
}
