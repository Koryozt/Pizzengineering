using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pizzengineering.Domain.Abstractions;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Persistence.Repositories;

public sealed class OrderRepository : IOrderRepository
{
	private readonly ApplicationDbContext _context;

	public OrderRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(Order order, CancellationToken cancellationToken)
	{
		await _context
				.Set<Order>()
					.AddAsync(order, cancellationToken);
	}

	public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
		await _context
				.Set<Order>()
					.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
	

	public async Task<Order?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken) =>
		await _context
				.Set<Order>()
					.FirstOrDefaultAsync(e => e.UserId == userId, cancellationToken);


	public void Update(Order order) =>
		_context
		.Set<Order>()
			.Update(order);
}
