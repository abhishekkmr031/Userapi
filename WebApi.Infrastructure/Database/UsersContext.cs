using WebApi.Common.Model;

namespace WebApi.Infrastructure.Database
{
	public class UsersContext
	{
		private List<User> userData = new List<User>();

		public void Add(User user)
		{
			userData.Add(user);
		}

		public IEnumerable<User> GetAllUsers()
		{
			return userData;
		}
	}
}
