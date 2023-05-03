using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzengineering.Domain.Enumerators;

public enum Permissions
{
	AccessUser = 1,
	ReadUser,
	CreateUser,	
	UpdateUser,
	DeleteUser,
	Administration,

	GetAllPizzas,
	ReadPizza,
	CreatePizza,
	UpdatePizza,
	DeletePizza,

	CreateOrder,
	UpdateOrder,
	DeleteOrder,

	ReadPaymentInformation,
	CreatePaymentInformation,
	UpdatePaymentInformation,
	DeletePaymentInformation
}
