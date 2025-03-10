using MediatR;
using WebApi.Common.Model;

namespace WebApi.Common.Commands
{
	public class GetUserByEmailIdQuery : IRequest<UserDto>
	{
		public string EmailId { get; set; }

		public GetUserByEmailIdQuery(string _emailid)
		{
			EmailId = _emailid;
		}
	}
}
