using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.PaymentInformation;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Payments.Commands.Create;

public sealed record CreatePaymentCommand(
		Guid UserId,
		string CreditCardNumber,
		string NameOnCard,
		DateTime ExpirationDate,
		string AddressLineOne,
		string? AddressLineTwo,
		string Country,
		string State,
		string City) : ICommand<Guid>;
