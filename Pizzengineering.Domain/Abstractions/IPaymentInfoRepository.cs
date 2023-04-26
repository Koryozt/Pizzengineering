using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Domain.Abstractions;

public interface IPaymentInfoRepository
{
	Task<PaymentInformation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task AddAsync(
		PaymentInformation paymentInformation, 
		CancellationToken cancellationToken);
	void Update(PaymentInformation paymentInformation);
}
