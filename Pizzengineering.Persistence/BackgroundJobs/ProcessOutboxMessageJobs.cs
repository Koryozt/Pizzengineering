using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pizzengineering.Domain.Primitives;
using Pizzengineering.Persistence.OutBox;
using Quartz;

namespace Pizzengineering.Persistence.BackgroundJobs;

public class ProcessOutboxMessagesJob : IJob
{
	private readonly ApplicationDbContext _context;
	private readonly IPublisher _publisher;

	public ProcessOutboxMessagesJob(IPublisher publisher, ApplicationDbContext context)
	{
		_publisher = publisher;
		_context = context;
	}

	public async Task Execute(IJobExecutionContext context)
	{
		List<OutboxMessage> messages = await _context
			.Set<OutboxMessage>()
			.Where(m => m.ProcessedOnUtc == null)
			.Take(20)
			.ToListAsync(context.CancellationToken);

		foreach (var outboxMessage in messages)
		{
			IDomainEvent? domainEvent = JsonConvert
				.DeserializeObject<IDomainEvent>(outboxMessage.Content);

			if (domainEvent is null)
			{
				continue;
			}

			await _publisher.Publish(domainEvent, context.CancellationToken);

			outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
		}

		await _context.SaveChangesAsync(context.CancellationToken);
	}
}