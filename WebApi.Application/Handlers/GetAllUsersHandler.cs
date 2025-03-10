using AutoMapper;
using MediatR;
using WebApi.Common.Commands;
using WebApi.Common.Model;
using WebApi.Infrastructure.Database;

namespace WebApi.Application.Handlers
{
	public class GetAllUsersHandler : IRequestHandler<GetAllUserQuery, IEnumerable<UserDto>>
	{
		private readonly UsersContext usersContext;
		private readonly IMapper mapper;
		public GetAllUsersHandler(UsersContext _usersContext, IMapper _mapper)
		{
			usersContext = _usersContext;
			mapper = _mapper;
		}
		public async Task<IEnumerable<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
		{
			var userDto = mapper.Map<IEnumerable<UserDto>>(usersContext.GetAllUsers());
			return await Task.FromResult(userDto);
		}
	}
}
