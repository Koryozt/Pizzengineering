﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Domain.Abstractions;

public interface IOrderRepository
{
	Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<Order?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
	Task AddAsync(Order order, CancellationToken cancellationToken);
	void Update(Order order);
}
