﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzengineering.Domain.Primitives;

public abstract class AggregateRoot : Entity
{
	private readonly List<IDomainEvent> _events = new();

	protected AggregateRoot(Guid id)
		: base(id)
	{
	}

	protected AggregateRoot()
	{
	}

	public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => this._events.ToList();
	public void ClearDomainEvents() => this._events.Clear();

	protected void RaiseDomainEvent(IDomainEvent domainEvent)
	{
		_events.Add(domainEvent);
	}
}
