﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.Shared;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Domain.Abstractions;

public interface IPizzaRepository
{
	Task<List<Pizza>> GetAllAsync(CancellationToken cancellationToken);
	Task<Pizza?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<Pizza?> GetByNameAsync(Name name, CancellationToken cancellation);
	Task<Result<Guid>> AddAsync(Pizza pizza, CancellationToken cancellationToken);
	void Update(Pizza pizza);
}
