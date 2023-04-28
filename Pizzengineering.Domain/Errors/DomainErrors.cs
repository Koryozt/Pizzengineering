using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Shared;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Domain.Errors;

public static class DomainErrors
{
	public static class Order
	{
		public static Error ExceptionInCreation = new(
			"Order.ExceptionInCreation",
			"There was a problem validating the user and/or there were no pizzas to buy");

		public static Error NotFound(Guid id) =>
			new Error(
				"Order.NotFound",
				"The order was not found. Verify if the OrderID or the UserID are correct");
	}

	public static class PaymentInformation
	{
		public static Error NotFound(Guid id) =>
			new Error(
				"PaymentInformation.NotFound",
				$"The payment information with ID {id} was not found");

		public static Error Invalid(string card, string name) =>
	new Error(
		"PaymentInformation.Invalid",
		"Could not create an instance of Payment Information. " +
		$"The name {name} or the card number {card} maybe are invalid");

		public static Error UserAlreadyWithPaymentInformation(Guid id) =>
			new Error(
				"PaymentInformation.UserAlreadyWithPaymentInformation",
				$"The user with ID {id} already has a valid payment information");
	}

	public static class Pizza
	{
		public static Error NotFound = 
			new Error(
			  "Pizza.NotFound",
			  $"The pizza does not exist. Verify the ID or the Name");

		public static Error Invalid(string name, double rate, string description) =>
			new Error(
				"Pizza.Invalid",
				"Could not create an instance of Pizza. " +
				$"The name {name}, the rate {rate} or the description {description} maybe are invalid");
	}

	public static class User
	{
		public static Error InvalidCredentials =
			new Error(
				"User.InvalidCredentials",
				"The values provided are not valid");

		public static Error NotFound(Guid id) =>
			new Error(
				"User.NotFound",
				$"The user with ID {id} was not found");
		public static Error EmailAlreadyInUse(string email) => 
			new Error(
			"User.EmailInUse",
			$"Email {email} already in use. Try another one");

		public static Error InvalidPassword(string password) => 
			new Error(
			"User.InvalidPassword",
			$"The password {password} is invalid, check the requeriments");
	}

	public static class Email
	{
		public static readonly Error Empty = new(
			"Email.Empty",
			"Email is empty");

		public static readonly Error TooLong = new(
			"Email.TooLong",
			"Email is too long");

		public static readonly Error InvalidFormat = new(
			"Email.InvalidFormat",
			"Email format is invalid");
	}

	public static class FirstName
	{
		public static readonly Error Empty = new(
			"FirstName.Empty",
			"First name is empty");

		public static readonly Error TooLong = new(
			"LastName.TooLong",
			"FirstName name is too long");
	}

	public static class LastName
	{
		public static readonly Error Empty = new(
			"LastName.Empty",
			"Last name is empty");

		public static readonly Error TooLong = new(
			"LastName.TooLong",
			"Last name is too long");
	}

	public static class Password
	{
		public static readonly Error Empty = new(
			"Password.Empty",
			"Password is empty");

		public static readonly Error TooShort = new(
			"Password.TooShort",
			"Password too short. The minimum permitted are 8 characters");

		public static readonly Error TooLong = new(
			"Password.TooLong",
			"Password too long. The maximum permitted are 32 characters");

		public static readonly Error Invalid = new(
			"Password.Invalid",
			"The password must contain uppercase, lowercase, numbers and special characters");
	}

	public static class Rate
	{
		public static readonly Error Invalid = new(
			"Rate.InvalidValue",
			"The rate value must be between 0.0 and 5.0");
	}

	public static class Description
	{
		public static readonly Error Empty = new(
			"Description.Empty",
			"Description is empty");

		public static readonly Error TooLong = new(
			"Description.TooLong",
			"The maximum permitted are 500 characters");
	}

	public static class CreditCardNumber
	{
		public static readonly Error Empty = new(
			"CreditCardNumber.Empty",
			"Card number is empty");

		public static readonly Error Invalid = new(
			"CreditCardNumber.Invalid",
			"Invalid credit card number");

		public static readonly Error NotOnlyDigits = new(
			"CreditCardNumber.NotOnlyDigits",
			"The credit card can not contain special characters or letters");
	}
}
