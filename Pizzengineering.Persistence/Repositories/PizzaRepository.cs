using Microsoft.EntityFrameworkCore;
using Pizzengineering.Domain.Abstractions;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Persistence.Repositories;

public class PizzaRepository : IPizzaRepository
{
	private readonly ApplicationDbContext _context;

	public PizzaRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(Pizza pizza, CancellationToken cancellationToken) =>
		await _context
				.Set<Pizza>()
					.AddAsync(pizza, cancellationToken);

	public async Task<List<Pizza>> GetAllAsync(CancellationToken cancellationToken) =>
		await _context
				.Set<Pizza>()
					.ToListAsync(cancellationToken);

	public async Task<Pizza?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
		await _context
				.Set<Pizza>()
					.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

	public async Task<Pizza?> GetByNameAsync(Name name, CancellationToken cancellationToken) =>
		await _context
				.Set<Pizza>()
					.FirstOrDefaultAsync(e => e.Name == name, cancellationToken);

	public void Update(Pizza pizza) =>
		_context
			.Set<Pizza>()
				.Update(pizza);
}
