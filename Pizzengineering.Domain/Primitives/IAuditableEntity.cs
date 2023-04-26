using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzengineering.Domain.Primitives;

public interface IAuditableEntity
{
	DateTime CreatedOnUtc { get; init; }
	DateTime? LastModifiedUtc { get; set; }
}
