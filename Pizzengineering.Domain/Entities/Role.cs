﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Primitives;

namespace Pizzengineering.Domain.Entities;

public sealed class Role : Enumeration<Role>
{
	public static readonly Role Registered = new(1, "Registered");


	public Role(int id, string name)
		: base(id, name)
	{

	}

	public ICollection<Permission> Permissions { get; set; }
	public ICollection<User> Users { get; set; }
}
