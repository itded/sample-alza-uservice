using System.Net;
using CSharpFunctionalExtensions;

namespace Alza.UService.Backend;

/// <summary>
/// The exception handler middleware
/// </summary>
internal class ExceptionHandler
{
    private readonly RequestDelegate _next;

    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleException(ex, httpContext);
        }
    }

    private async Task HandleException(Exception ex, HttpContext httpContext)
    {
        if (ex is InvalidOperationException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await httpContext.Response.WriteAsJsonAsync(new
            {
                Message = "Invalid operation",
            });
        }
        else if (ex is ResultFailureException)
        {
            var failureException = (ResultFailureException)ex;
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await httpContext.Response.WriteAsJsonAsync(new
            {
                Message = $"Invalid domain operation: {failureException.Error}",
            });
        }
        else
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsync("Unknown error");
        }


    }
}
