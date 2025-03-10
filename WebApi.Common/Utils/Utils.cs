
namespace WebApi.Common.Utils
{
	public static class Utils
	{
		public static int CalculateAge(DateTime dob)
		{
			var today = DateTime.Today;
			var age = today.Year - dob.Year;
			if (dob.Date > today.AddYears(-age)) age--;
			return age;
		}
	}
}
