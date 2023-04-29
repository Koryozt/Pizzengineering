using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
	private readonly IHttpContextAccessor _accessor;
	private readonly IOrderRepository _repository;
	private readonly IUserRepository _userRepository;
	private readonly IPizzaRepository _pizzaRepository;
	private readonly IUnitOfWork _uow;

	public OrderCommandsHandler(
		IHttpContextAccessor accessor,
		IOrderRepository repository,
		IUserRepository userRepository,
		IPizzaRepository pizzaRepository,
		IUnitOfWork uow)
	{
		_accessor = accessor;
		_repository = repository;
		_userRepository = userRepository;
		_pizzaRepository = pizzaRepository;
		_uow = uow;
	}

	public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
	{
		Guid id = Guid.Parse(
			_accessor
			.HttpContext
			.User
			.FindFirst(JwtRegisteredClaimNames.Sub)!.Value);

		User? user = await _userRepository.GetByIdAsync(id, cancellationToken);

		if (user is null)
		{
			return Result.Failure<Guid>(
				DomainErrors
				.User
				.NotFound(id));
		}

		List<Pizza> pizzas = new();

		request.PizzaIds
			.ToList()
			.ForEach(async p =>
			{
				var pizza = await _pizzaRepository.GetByIdAsync(p, cancellationToken);
				
				if (pizza is null) 
				{
					return;
				}

				pizzas.Add(pizza);
			});

		var order = Order.Create(
			Guid.NewGuid(),
			user,
			pizzas,
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
