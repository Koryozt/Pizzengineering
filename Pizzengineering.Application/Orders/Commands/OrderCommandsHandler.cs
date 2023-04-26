using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Application.Orders.Commands.Create;
using Pizzengineering.Domain.Abstractions;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Application.Orders.Commands;

public sealed class OrderCommandsHandler :
	ICommandHandler<CreateOrderCommand, Guid>
{
	private readonly IOrderRepository _repository;
	private readonly IUnitOfWork _uow;

	public OrderCommandsHandler(IOrderRepository repository, IUnitOfWork uow)
	{
		_repository = repository;
		_uow = uow;
	}

	public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
	{
		var order = Order.Create(
			Guid.NewGuid(),
			request.User,
			request.Pizzas,
			request.PurchasedAtUtc);

		if (order is null)
		{
			return Result.Failure<Guid>(DomainErrors.Order.ExceptionInCreation);
		}

		await _repository.AddAsync(order, cancellationToken);
		await _uow.SaveChangesAsync(cancellationToken);

		return Result.Success(order.Id);
	}
}
