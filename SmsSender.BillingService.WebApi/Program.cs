using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using SmsSender.BillingService.CQRS.Bootstrap;
using SmsSender.BillingService.CQRS.SmsProfile.Queries.GetById;
using SmsSender.BillingService.WebApi.Middlewares;
using System.Reflection;
using SmsSender.Common.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((context, config) =>
{
    var environment = context.HostingEnvironment;

    config.SetBasePath(environment.ContentRootPath)
          .AddJsonFile("appsettings.json", false, true)
          .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true)
          .AddEnvironmentVariables()
          .AddUserSecrets(Assembly.GetExecutingAssembly());
});

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<GetSmsProfileByIdQueryValidator>();
builder.Services.AddAutoMapper(typeof(Program).Assembly,
    typeof(GetSmsProfileByIdQueryValidator).Assembly);
builder.Services.ConfigureRabbit(builder.Configuration);
builder.Services.UseCQRS(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware(typeof(ErrorHandlingMiddleware));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
