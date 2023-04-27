using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Application.Users.Commands.Login;
using Pizzengineering.Application.Users.Commands.Register;
using Pizzengineering.Application.Users.Commands.Update;
using Pizzengineering.Domain.Abstractions;
using Pizzengineering.Domain.DomainEvents;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Application.Users.Commands;

public sealed class UserCommandsHandler : 
	ICommandHandler<CreateUserCommand, Guid>,
	ICommandHandler<LoginCommand, string>,
	ICommandHandler<UpdateUserCommand>
{
	private readonly IUserRepository _repository;
	private readonly IUnitOfWork _uow;

	public UserCommandsHandler(IUnitOfWork uow, IUserRepository repository)
	{
		_repository = repository;
		_uow = uow;
	}

	// Register
	public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		bool isUnique = await _repository.IsEmailInUseAsync(request.Email, cancellationToken);
		
		if (!isUnique) 
		{
			return Result.Failure<Guid>(DomainErrors.User.EmailAlreadyInUse(request.Email.Value));
		}

		var user = User.Create(
			Guid.NewGuid(),
			request.Firstname,
			request.Lastname,
			request.Email,
			request.Password);

		if (user is null)
		{
			return Result.Failure<Guid>(DomainErrors.User.InvalidPassword(request.Password.Value));
		}

		await _repository.AddAsync(user, cancellationToken);
		await _uow.SaveChangesAsync(cancellationToken);

		return Result.Success(user.Id);
	}

	//Login
	public Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	//Update
	public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
	{
		var user = await _repository.GetByIdAsync(request.Id, cancellationToken);

		if (user is null)
		{
			return Result.Failure(
				DomainErrors.User.NotFound(request.Id));
		}

		user.ChangeNames(
			request.Firstname, 
			request.Lastname);

		_repository.Update(user, cancellationToken);

		await _uow.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}
