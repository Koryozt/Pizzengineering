using Pizzengineering.Application.Abstractions.Messaging;
using Pizzengineering.Application.Users.Queries.GetUserById;
using Pizzengineering.Domain.Abstractions;
using Pizzengineering.Domain.Errors;
using Pizzengineering.Domain.Shared;

namespace Pizzengineering.Application.Users.Queries;

public sealed class UserQueriesHandler :
	IQueryHandler<GetUserByIdQuery, UserResponse>
{
	private readonly IUserRepository _repository;

	public UserQueriesHandler(IUserRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		var user = await _repository.GetByIdAsync(request.Id, cancellationToken);

		if (user is null)
		{
			return Result.Failure<UserResponse>(DomainErrors.User.NotFound(request.Id));
		}

		var response = new UserResponse(
			user.Id,
			user.Firstname,
			user.Lastname,
			user.Email,
			user.Password,
			user.PaymentInformation,
			user.OrdersMade);

		return Result.Success(response);
	}
}
