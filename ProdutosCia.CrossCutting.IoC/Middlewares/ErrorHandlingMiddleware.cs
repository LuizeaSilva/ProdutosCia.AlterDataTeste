using FluentValidation;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProdutosCia.Domain.Exceptions;

namespace ProdutosCia.CrossCutting.IoC.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate _next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        if (exception is BaseException)
        {
            var ex = exception as BaseException;
            response.StatusCode = ex.StatusCode;
            
            if (exception is UnauthorizedException)
            {
                return response.WriteAsJsonAsync(new ValidationProblemDetails(ex.Errors)
                {
                    Type = ex.Type,
                    Title = ex.Title,
                    Detail = ex.Detail,
                });
            }
            
            return response.WriteAsJsonAsync(new ProblemDetails
            {
                Type = ex.Type,
                Title = ex.Title,
                Detail = ex.Detail,
            });
        }
        
        if (exception is ValidationException)
        {
            response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            var ex = exception as ValidationException;
            var erros = ex.Errors.GroupBy(x => x.PropertyName)
                .ToDictionary(
                    x => x.Key,
                    x => x.Select(x => x.ErrorMessage.Replace("'", "")).ToArray());
            
            return response.WriteAsJsonAsync(new ValidationProblemDetails(erros)
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-422-unprocessable-content",
                Title = "One or more validation errors occurred.",
                Detail = "See Errors property for details.",
            });
        }
        
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return response.WriteAsJsonAsync(new ProblemDetails
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-500-internal-server-error",
            Title = "An unexpected condition has been encountered. Please try again later or contact support for assistance.",
            Detail = exception.Message,
        });
    }
}

public static class ErrorHandlingMiddlewareExtensions
{
    public static void UseErrorHandlingMiddleware(this IApplicationBuilder appBuilder)
    {
        appBuilder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}