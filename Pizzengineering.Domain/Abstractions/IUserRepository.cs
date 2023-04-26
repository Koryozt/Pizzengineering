using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Domain.Abstractions;

public interface IUserRepository
{
	Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task AddAsync(User user, CancellationToken cancellationToken);
	Task<bool> IsEmailInUseAsync(Email email, CancellationToken cancellationToken);
	void Update(User user, CancellationToken cancellationToken);
}
