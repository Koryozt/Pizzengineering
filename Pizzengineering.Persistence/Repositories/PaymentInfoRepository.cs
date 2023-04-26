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

public class PaymentInfoRepository : IPaymentInfoRepository
{
	private readonly ApplicationDbContext _context;

	public PaymentInfoRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(PaymentInformation paymentInformation, CancellationToken cancellationToken) =>
		await _context
				.Set<PaymentInformation>()
					.AddAsync(paymentInformation, cancellationToken);

	public async Task<PaymentInformation?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
		await _context
				.Set<PaymentInformation>()
					.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

	public void Update(PaymentInformation paymentInformation) =>
		_context 
			.Set<PaymentInformation>()
				.Update(paymentInformation);
}
