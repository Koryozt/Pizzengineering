using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.PaymentInformation;
using Pizzengineering.Domain.ValueObjects.User;
using Pizzengineering.Persistence.Constants;

namespace Pizzengineering.Persistence.Configuration;

public sealed class PaymentInformationConfiguration :
	IEntityTypeConfiguration<PaymentInformation>
{
	public void Configure(EntityTypeBuilder<PaymentInformation> builder)
	{
		builder.ToTable(TableNames.Payments);

		builder.HasKey(e => e.Id);

		builder.HasOne(e => e.User)
				.WithOne(e => e.PaymentInformation)
				.HasForeignKey<PaymentInformation>(e => e.UserId);

		builder.Property(e => e.CardNumber)
			.HasConversion(x => x.Value, v => CreditCardNumber.Create(v).Value);

		builder.Property(e => e.NameOnCard)
			.HasConversion(x => x.Value, v => Name.Create(v).Value)
			.HasMaxLength(Name.MaxLength);
	}
}
