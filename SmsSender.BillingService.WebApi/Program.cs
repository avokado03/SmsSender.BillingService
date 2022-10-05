using FluentValidation.AspNetCore;
using SmsSender.BillingService.CQRS.Bootstrap;
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


builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssemblyContaining<SmsProfileValidator>());
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.UseCQRS(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
