using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Common.Model
{
	public class UserDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string? NickName { get; set; }
		public int? FriendCount { get; set; }
		public int Age => Utils.Utils.CalculateAge(DateOfBirth);
	}
}
