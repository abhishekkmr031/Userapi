using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Common.Commands;
using WebApi.Common.Validations;

namespace WebApi.API.Controllers
{
	[ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiVersion("1.0")]
	public class UserController
	{
		private readonly IMediator _mediator;

		public UserController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[SwaggerOperation(Summary = "Creates a new user", Description = "Requires unique email and age 18+")]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
		{
			var validator = new UserValidator();
			var validationResult = await validator.ValidateAsync(command);

			if (!validationResult.IsValid)
			{
				return new BadRequestObjectResult(validationResult.Errors);
			}

			var result = await _mediator.Send(command);
			return new OkObjectResult(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _mediator.Send(new GetAllUserQuery());

			if (users is null)
			{
				return new NotFoundObjectResult("users not available");
			}

			return new OkObjectResult(users);
		}

		[HttpGet("{emailid}")]
		public async Task<IActionResult> GetUserById(string emailid)
		{
			var user = await _mediator.Send(new GetUserByEmailIdQuery(emailid));
			if (user is null)
			{
				return new NotFoundObjectResult("users not available");
			}
			return new OkObjectResult(user);
		}

	}
}
