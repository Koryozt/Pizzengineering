using Pizzengineering.Domain.Entities;

namespace Pizzengineering.Domain.Abstractions;

public interface IOrderRepository
{
	Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<Order?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
	Task AddAsync(Order order, CancellationToken cancellationToken);
	void Update(Order order);
}
