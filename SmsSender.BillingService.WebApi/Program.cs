using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using SmsSender.BillingService.CQRS.Bootstrap;
using SmsSender.BillingService.CQRS.SmsProfile.Queries.GetById;
using SmsSender.BillingService.WebApi.Middlewares;
using System.Reflection;

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
builder.Services.UseCQRS(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "SmsSender.BillingService", Version = "v1" 
    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("./v1/swagger.json", "SmsSender.BillingService V1");
});

app.UseMiddleware(typeof(ErrorHandlingMiddleware));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
