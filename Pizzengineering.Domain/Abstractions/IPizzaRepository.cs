using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Domain.Abstractions;

public interface IPizzaRepository
{
	Task<List<Pizza>> GetAllAsync(CancellationToken cancellationToken);
	Task<Pizza?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<Pizza?> GetByNameAsync(Name name, CancellationToken cancellationToken);
	Task AddAsync(Pizza pizza, CancellationToken cancellationToken);
	void Update(Pizza pizza);
}
