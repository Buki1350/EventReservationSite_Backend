using DotNetBoilerplate.Shared.Abstractions.Exceptions;
using DotNetBoilerplate.Shared.Abstractions.Exceptions.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace DotNetBoilerplate.Infrastructure.Exceptions;

internal sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            await HandleExceptionAsync(e, context);
        }
    }

    private async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        var (statusCode, error) = exception switch
        {
            CustomException => (StatusCodes.Status400BadRequest,
                new ErrorsResponse(new Error(exception.GetType().Name.Replace("Exception", string.Empty),
                    exception.Message))),

            BusinessRuleValidationException => (StatusCodes.Status400BadRequest,
                new ErrorsResponse(new Error(exception.GetType().Name.Replace("Exception", string.Empty),
                    exception.Message))),

            ValidationException validationException => (StatusCodes.Status400BadRequest,
                new ErrorsResponse(validationException.Errors.Select(
                        e => new Error(e.ErrorCode, e.ErrorMessage, e.PropertyName)).ToArray()
                )),

            _ => (StatusCodes.Status500InternalServerError,
                new ErrorsResponse(new Error("error", "Something went wrong")))
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }
}