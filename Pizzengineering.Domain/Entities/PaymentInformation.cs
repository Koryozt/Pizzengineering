using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Primitives;
using Pizzengineering.Domain.ValueObjects.PaymentInformation;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Domain.Entities;

public sealed class PaymentInformation	: AggregateRoot, IAuditableEntity
{
	public PaymentInformation(
		Guid id, 
		User user,
		CreditCardNumber creditCardNumber, 
		Name nameOnCard,
		DateTime expirationDate,
		string addressLineOne,
		string? addressLineTwo,
		string country,
		string state,
		string city) : base(id)
	{
		User = user;
		CardNumber = creditCardNumber;
		NameOnCard = nameOnCard;
		ExpirationDate = expirationDate;
		AddressLineOne = addressLineOne;
		AddressLineTwo = addressLineTwo;
		Country = country;
		State = state;
		City = city;
	}

	public Guid UserGuid { get; private set; }
	public User User { get; set; }

	public CreditCardNumber CardNumber { get; private set; }
	public Name	NameOnCard { get; private set; }
	public DateTime ExpirationDate { get; private set; }

	public string AddressLineOne { get; private set; }
	public string? AddressLineTwo { get; private set; }
	public string Country { get; private set; }
	public string State { get; private set; }
	public string City { get; private set; }
	public DateTime CreatedOnUtc { get; init; }
	public DateTime? LastModifiedUtc { get; set; }
}
