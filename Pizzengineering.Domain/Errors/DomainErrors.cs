using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Domain.Errors;

public static class DomainErrors
{
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
