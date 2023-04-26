﻿using Pizzengineering.Domain.ValueObjects.PaymentInformation;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Payments.Queries;

public sealed record PaymentResponse(
	Guid id,
	Guid UserId,
	CreditCardNumber CreditCardNumber,
	Name NameOnCard,
	DateTime ExpirationDate,
	string AddressLineOne,
	string? AddressLineTwo,
	string Country,
	string State,
	string City);
