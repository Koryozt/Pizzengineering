using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzengineering.Persistence.Constants;
using Pizzengineering.Persistence.OutBox;

namespace Pizzengineering.Persistence.Configuration;

public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
	public void Configure(EntityTypeBuilder<OutboxMessage> builder)
	{
		builder.ToTable(TableNames.Outbox);

		builder.HasKey(x => x.Id);
	}
}