using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using WebApi.Common.Commands;

namespace WebApi.Common.Validations
{
	public class UserValidator : AbstractValidator<CreateUserCommand>
	{
		public UserValidator()
		{
			RuleFor(user => user.FirstName)
				.NotEmpty().WithMessage("FirstName is required.");
			RuleFor(user => user.LastName)
				.NotEmpty().WithMessage("LastName is required.");
			RuleFor(user => user.Email)
				.NotEmpty().WithMessage("Email is required.")
				.EmailAddress().WithMessage("Email is not proper");
			RuleFor(user => user.DateOfBirth)

			.NotEmpty().WithMessage("Date of Birth is required.")
			.Must(BeAtLeast18YearsOld).WithMessage("User must be at least 18 years old.");

			RuleFor(user => user.FriendCount)
				.GreaterThanOrEqualTo(0).WithMessage("Friend Count cannot be less than 0.");
		}

		private bool BeAtLeast18YearsOld(DateTime dob)
		{
			var today = DateTime.Today;
			var age = today.Year - dob.Year;

			if (dob.Date > today.AddYears(-age))
			{
				age--;
			}

			return age >= 18;
		}
	}



}