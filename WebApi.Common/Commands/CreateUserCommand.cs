using MediatR;
using WebApi.Common.Model;

namespace WebApi.Common.Commands
{
	public class CreateUserCommand : IRequest<UserDto>
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Email { get; set; }
		public required DateTime DateOfBirth { get; set; }
		public string? NickName { get; set; }
		public int FriendCount { get; set; }
	}
}
