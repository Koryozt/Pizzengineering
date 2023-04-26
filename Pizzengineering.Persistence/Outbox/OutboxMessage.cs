﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzengineering.Persistence.OutBox;

public sealed class OutboxMessage
{
	public Guid Id { get; set; }

	public string Type { get; set; } = string.Empty;

	public string Content { get; set; } = string.Empty;

	public DateTime OcurredOnUtc { get; set; }

	public DateTime? ProcessedOnUtc { get; set; }

	public string? Error { get; set; }
}