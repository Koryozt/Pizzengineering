using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.Pizza;
using Pizzengineering.Domain.ValueObjects.User;
using Pizzengineering.Persistence.Constants;

namespace Pizzengineering.Persistence.Configuration;

public sealed class PizzaConfiguration : IEntityTypeConfiguration<Pizza>
{
	public void Configure(EntityTypeBuilder<Pizza> builder)
	{
		builder.ToTable(TableNames.Pizzas);

		builder.HasKey(e => e.Id);

		builder.Property(e => e.Name)
				.HasConversion(x => x.Value, v => Name.Create(v).Value)
				.HasMaxLength(Name.MaxLength);

		builder.Property(e => e.Description)
				.HasConversion(x => x.Value, v => Description.Create(v).Value)
				.HasMaxLength(Description.MaxValue);

		builder.Property(e => e.Rate)
				.HasConversion(x => x.Value, v => Rate.Create(v).Value);
	}
}
