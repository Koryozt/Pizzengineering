using Pizzengineering.Domain.Entities;

namespace Pizzengineering.Application.Abstractions;

public interface IEmailService
{
	Task SendWelcomeEmailAsync(User member, CancellationToken cancellationToken = default);

	Task SendOrderConfirmationEmailAsync(User member, Order order, CancellationToken cancellationToken = default);
}
