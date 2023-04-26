using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Domain.ValueObjects.PaymentInformation;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Payments.Commands.Update;

public sealed record UpdatePaymentCommand(
	Guid Id,
	CreditCardNumber CreditCardNumber,
	Name NameOnCard,
	DateTime ExpirationDate,
	string AddressLineOne,
	string? AddressLineTwo,
	string Country,
	string State,
	string City) : ICommand;
