using System.Net;
using System.Text.Json;
using FluentValidation;
using WebApi.Common.Model;

namespace WebApi.API.Middlewares
{
	public class ExceptionMiddleware
	{

		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				LogErrorDetails(context, ex);
				await HandleExceptionAsync(context, ex);
			}
		}

		private void LogErrorDetails(HttpContext context, Exception exception)
		{
			_logger.LogError(exception, "Error occurred while processing request: {Method} {Path}",
				context.Request.Method, context.Request.Path);
		}

		private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var response = context.Response;
			response.ContentType = "application/json";

			var errorResponse = exception switch
			{
				ValidationException validationEx => new ErrorResponse
				{
					StatusCode = (int)HttpStatusCode.BadRequest,
					Error = "Validation Error",
					Details = validationEx.Errors.Select(e => e.ErrorMessage).ToList()
				},
				KeyNotFoundException => new ErrorResponse
				{
					StatusCode = (int)HttpStatusCode.NotFound,
					Error = "Not Found",
					Details = new List<string> { exception.Message }
				},
				_ => new ErrorResponse
				{
					StatusCode = (int)HttpStatusCode.InternalServerError,
					Error = "Internal Server Error",
					Details = new List<string> { "An unexpected error occurred." }
				}
			};

			response.StatusCode = errorResponse.StatusCode;
			var jsonResponse = JsonSerializer.Serialize(errorResponse);
			await response.WriteAsync(jsonResponse);
		}
	}
}