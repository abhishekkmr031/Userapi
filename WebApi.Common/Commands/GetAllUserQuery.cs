using MediatR;
using WebApi.Common.Model;

namespace WebApi.Common.Commands
{
	public class GetAllUserQuery : IRequest<IEnumerable<UserDto>>
	{
	}
}
