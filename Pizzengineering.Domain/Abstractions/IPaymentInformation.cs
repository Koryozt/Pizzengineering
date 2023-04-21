﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Domain.Abstractions;

public interface IPaymentInformation
{
	Task<PaymentInformation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<Result<Guid>> AddAsync(
		PaymentInformation paymentInformation, 
		CancellationToken cancellationToken);
	void Update(PaymentInformation paymentInformation);
}
