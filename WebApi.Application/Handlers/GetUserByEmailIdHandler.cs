using AutoMapper;
using MediatR;
using WebApi.Common.Commands;
using WebApi.Common.Model;
using WebApi.Infrastructure.Database;

namespace WebApi.Application.Handlers
{
	public class GetUserByEmailIdHandler : IRequestHandler<GetUserByEmailIdQuery, UserDto>
	{
		private readonly UsersContext usersContext;
		private readonly IMapper mapper;
		public GetUserByEmailIdHandler(UsersContext _usersContext, IMapper _mapper)
		{
			usersContext = _usersContext;
			mapper = _mapper;
		}

		public async Task<UserDto> Handle(GetUserByEmailIdQuery request, CancellationToken cancellationToken)
		{
			var user = usersContext.GetAllUsers().FirstOrDefault(u => u.Email == request.EmailId);

			if (user == null)
			{
				throw new KeyNotFoundException($"User with ID {request.EmailId} not found.");
			}

			var userDto = mapper.Map<UserDto>(user);
			return await Task.FromResult(userDto);
		}
	}
}
