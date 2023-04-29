using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzengineering.Application.Abstractions;
using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Application.Users.Commands.Login;
using Pizzengineering.Application.Users.Commands.Register;
using Pizzengineering.Application.Users.Commands.Update;
using Pizzengineering.Domain.Abstractions;
using Pizzengineering.Domain.DomainEvents;
using Pizzengineering.Domain.Entities;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Shared;
using Pizzengineering.Domain.ValueObjects.User;

namespace Pizzengineering.Application.Users.Commands;

public sealed class UserCommandsHandler : 
	ICommandHandler<CreateUserCommand, Guid>,
	ICommandHandler<LoginCommand, string>,
	ICommandHandler<UpdateUserCommand>
{
	private readonly IUserRepository _repository;
	private readonly IJwtProvider _provider;
	private readonly IUnitOfWork _uow;

	//luissilva2010@gmail.com

	public UserCommandsHandler(IUnitOfWork uow, IUserRepository repository, IJwtProvider provider)
	{
		_repository = repository;
		_uow = uow;
		_provider = provider;
	}

	// Register
	public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		Result<Email> emailResult = Email.Create(request.Email);

		bool isNotUnique = await _repository.IsEmailInUseAsync(emailResult.Value, cancellationToken);
		
		if (isNotUnique) 
		{
			return Result.Failure<Guid>(DomainErrors.User.EmailAlreadyInUse(request.Email));
		}

		Result<Name> firstnameResult = Name.Create(request.Firstname);
		Result<Name> lastnameResult = Name.Create(request.Lastname);
		Result<Password> passwordResult = Password.Create(request.Password);

		if (firstnameResult.IsFailure || 
			lastnameResult.IsFailure ||
			emailResult.IsFailure ||
			passwordResult.IsFailure)
		{
			return Result.Failure<Guid>(
				DomainErrors
				.User
				.InvalidCredentials);
		}

		var user = User.Create(
			Guid.NewGuid(),
			firstnameResult.Value,
			lastnameResult.Value,
			emailResult.Value,
			passwordResult.Value);

		if (user is null)
		{
			return Result.Failure<Guid>(DomainErrors.User.InvalidPassword(request.Password));
		}

		await _repository.AddAsync(user, cancellationToken);
		await _uow.SaveChangesAsync(cancellationToken);

		return Result.Success(user.Id);
	}

	//Login
	public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
	{
		Result<Email> email = Email.Create(request.Email);

		User? member = await _repository.GetByEmailAsync(email.Value, cancellationToken);

		if (member is null)
		{
			return Result.Failure<string>(
				DomainErrors.User.InvalidCredentials);
		}
		
		string token = _provider.Generate(member);

		return Result.Success(token);
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

		Result<Name> firstnameResult = Name.Create(request.Firstname),
			lastnameResult = Name.Create(request.Lastname);

		user.ChangeNames(
			firstnameResult.Value, 
			lastnameResult.Value);

		_repository.Update(user);

		await _uow.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}
