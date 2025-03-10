using FluentValidation;
using MediatR;
using WebApi.Common.Commands;
using WebApi.Common.Model;
using WebApi.Infrastructure.Database;

namespace WebApi.Application.Handlers
{
	public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
	{
		private readonly UsersContext usersContext;
		public CreateUserHandler(UsersContext _usersContext)
		{
			usersContext = _usersContext;
		}

		public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var user = new User
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email,
				DateOfBirth = request.DateOfBirth,
				NickName = request.NickName,
				FriendCount = request.FriendCount
			};

			if (usersContext.GetAllUsers().Any(u => u.Email == request.Email))
			{
				throw new ValidationException("Email must be unique.");
			}

			usersContext.Add(user);
			return await Task.FromResult(new UserDto
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				DateOfBirth = user.DateOfBirth,
				NickName = user.NickName,
				FriendCount = user.FriendCount
			});
		}
	}
}
