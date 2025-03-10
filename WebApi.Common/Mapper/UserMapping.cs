using AutoMapper;
using WebApi.Common.Model;

namespace WebApi.Common.Mapper
{
	public class UserMappingProfile : Profile
	{
		public UserMappingProfile()
		{
			CreateMap<User, UserDto>()
				.ForMember(dest => dest.Age, opt => opt.MapFrom(src => Utils.Utils.CalculateAge(src.DateOfBirth)));
		}
	}
}
