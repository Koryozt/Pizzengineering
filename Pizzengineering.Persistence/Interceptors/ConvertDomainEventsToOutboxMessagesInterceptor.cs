using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using Pizzengineering.Domain.Primitives;
using Pizzengineering.Persistence.OutBox;

namespace Pizzengineering.Persistence.Interceptors;

public sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
		DbContextEventData eventData,
		InterceptionResult<int> result,
		CancellationToken cancellationToken
		)
	{
		DbContext? context = eventData.Context;

		if (context is null)
		{
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		var outboxMessages = context.ChangeTracker
			.Entries<AggregateRoot>()
			.Select(x => x.Entity)
			.SelectMany(aggregateRoot =>
			{
				var domainEvents = aggregateRoot.GetDomainEvents();

				aggregateRoot.ClearDomainEvents();

				return domainEvents;
			})
			.Select(domainEvent => new OutboxMessage()
			{
				Id = Guid.NewGuid(),
				Type = domainEvent.GetType().Name,
				Content = JsonConvert.SerializeObject(domainEvent,
				new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.All
				}),
				OcurredOnUtc = DateTime.UtcNow,
			})
			.ToList();

		context.Set<OutboxMessage>()
			.AddRange(outboxMessages);

		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}
}