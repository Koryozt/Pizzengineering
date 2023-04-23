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

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.ToTable(TableNames.Orders);

		builder.HasKey(e => e.Id);

		builder.HasOne(e => e.User)
				.WithMany(e => e.OrdersMade)
				.HasForeignKey(e => e.UserId);

		builder.Ignore(e => e.TotalPrice);
	}
}
