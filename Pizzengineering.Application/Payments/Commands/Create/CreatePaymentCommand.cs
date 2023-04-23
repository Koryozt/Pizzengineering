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
		User User,
		CreditCardNumber CreditCardNumber,
		Name NameOnCard,
		DateTime ExpirationDate,
		string AddressLineOne,
		string? AddressLineTwo,
		string Country,
		string State,
		string City) : ICommand<Guid>;
