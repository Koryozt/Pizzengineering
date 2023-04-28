using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pizzengineering.Domain.Abstractions;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.Shared;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Persistence.Repositories;

public class UserRepository : IUserRepository
{
	private readonly ApplicationDbContext _context;

	public UserRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(User user, CancellationToken cancellationToken) =>
		await _context
			.Set<User>()
				.AddAsync(user, cancellationToken);

	public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken) =>
		await _context
			.Set<User>()
				.FirstOrDefaultAsync(e => e.Email == email, cancellationToken);

	public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
		await _context
			.Set<User>()
				.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

	public async Task<bool> IsEmailInUseAsync(Email email, CancellationToken cancellationToken) =>
		await _context
			.Set<User>()
				.AnyAsync(e => e.Email == email);
	

	public void Update(User user) =>
		_context
			.Set<User>()
				.Update(user);
}
