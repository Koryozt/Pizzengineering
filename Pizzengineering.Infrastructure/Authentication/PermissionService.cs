using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Persistence;

namespace Pizzengineering.Infrastructure.Authentication;

public class PermissionService : IPermissionService
{
	private readonly ApplicationDbContext _context;

	public PermissionService(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<HashSet<string>> GetPermissionsAsync(Guid userId)
	{
		ICollection<Role>[] roles = await _context.Set<User>()
			.Include(x => x.Roles)
			.ThenInclude(x => x.Permissions)
			.Where(x => x.Id == userId)
			.Select(x => x.Roles)
			.ToArrayAsync();

		return roles
			.SelectMany(x => x)
			.SelectMany(x => x.Permissions)
			.Select(x => x.Name)
			.ToHashSet<string>();
	}
}
