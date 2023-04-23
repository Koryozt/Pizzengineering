using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;

namespace Pizzengineering.Application.Payments.Queries.GetByCondition;

public sealed record GetPaymentByIdQuery(
	Guid Id) : IQuery<PaymentResponse>;
