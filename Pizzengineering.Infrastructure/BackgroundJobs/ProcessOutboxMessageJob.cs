using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using Pizzengineering.Domain.Primitives;
using Pizzengineering.Persistence.OutBox;
using Pizzengineering.Persistence;
using Quartz;
using Microsoft.EntityFrameworkCore;

namespace Pizzengineering.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
	private readonly ApplicationDbContext _dbContext;
	private readonly IPublisher _publisher;

	public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher)
	{
		_dbContext = dbContext;
		_publisher = publisher;
	}

	public async Task Execute(IJobExecutionContext context)
	{
		List<OutboxMessage> messages = await _dbContext
			.Set<OutboxMessage>()
			.Where(m => m.ProcessedOnUtc == null)
			.Take(20)
			.ToListAsync(context.CancellationToken);

		foreach (OutboxMessage outboxMessage in messages)
		{
			IDomainEvent? domainEvent = JsonConvert
				.DeserializeObject<IDomainEvent>(
					outboxMessage.Content,
					new JsonSerializerSettings
					{
						TypeNameHandling = TypeNameHandling.All
					});

			if (domainEvent is null)
			{
				continue;
			}

			await _publisher.Publish(domainEvent, context.CancellationToken);

			outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
		}

		await _dbContext.SaveChangesAsync();
	}
}