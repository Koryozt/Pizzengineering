using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Domain.Abstractions;

public interface IUserRepository
{
	Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<Result<Guid>> AddAsync(User user, CancellationToken cancellationToken);
	Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken);
	void Update(User user, CancellationToken cancellationToken);
}
