using FluentValidation;
using Newtonsoft.Json;
using System.Net;

namespace SmsSender.BillingService.WebApi.Middlewares;

/// <summary>
/// Промежуточный слой для кастомной обработки исключений.
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception.GetType() == typeof(ValidationException))
        {
            var code = HttpStatusCode.BadRequest;
            var result = JsonConvert.SerializeObject(((ValidationException)exception).Errors
                .Select(x => new { Title = "Ошибка валидации", Errors = new { x.ErrorCode, x.PropertyName, x.ErrorMessage}}));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);

        }
        else
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new { isSuccess = false, error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
