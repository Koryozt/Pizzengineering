using Pizzengineering.Domain.Entities;

namespace Pizzengineering.Domain.Abstractions;

public interface IPaymentInfoRepository
{
	Task<PaymentInformation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task AddAsync(
		PaymentInformation paymentInformation,
		CancellationToken cancellationToken);
	void Update(PaymentInformation paymentInformation);
}
