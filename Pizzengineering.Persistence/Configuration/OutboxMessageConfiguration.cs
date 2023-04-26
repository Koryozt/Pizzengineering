using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pizzengineering.Persistence.OutBox;
using Pizzengineering.Persistence.Constants;

namespace Pizzengineering.Persistence.Configuration;

public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
	public void Configure(EntityTypeBuilder<OutboxMessage> builder)
	{
		builder.ToTable(TableNames.Outbox);

		builder.HasKey(x => x.Id);
	}
}