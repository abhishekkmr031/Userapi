using FluentValidation;
using WebApi.Common.Validations;
using WebApi.Application.Handlers;
using WebApi.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using WebApi.Infrastructure.Database;
using WebApi.API.Middlewares;
using AutoMapper;
using WebApi.Common.Utils;
using WebApi.Common.Mapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(options =>
{
	options.ReportApiVersions = true;
	options.AssumeDefaultVersionWhenUnspecified = true;
	options.DefaultApiVersion = new ApiVersion(1, 0);
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllUserQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUserByEmailIdQuery).Assembly));
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddSingleton<UsersContext>();
builder.Services.AddAutoMapper(typeof(UserMappingProfile));

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.Run();
