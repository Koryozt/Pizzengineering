using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzengineering.Domain.Enumerators;

public enum Permissions
{
	AccessUser = 1,
	ReadUser = 2,
	CreateUser = 3,	
	UpdateUser = 4,
	DeleteUser = 5,

	GetAllPizzas = 6,
	ReadPizza = 7,
	CreatePizza = 8,
	UpdatePizza = 9,
	DeletePizza = 10,
}
