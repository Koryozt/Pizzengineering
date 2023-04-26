﻿namespace Pizzengineering.Domain.Abstractions;

public interface IUnitOfWork
{
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
