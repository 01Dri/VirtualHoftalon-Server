using System.Text.Json;

namespace VirtualHoftalon_Server.Exceptions;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

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
        var statusCode = StatusCodes.Status500InternalServerError; // Internal Server Error por padrão

        if (exception is NotFoundSectorException)
        {
            statusCode = StatusCodes.Status404NotFound;
        }
        
        if (exception is NotFoundDoctorException)
        {
            statusCode = StatusCodes.Status404NotFound;
        }
        
        if (exception is InvalidArgumentsUpdateSectorException)
        {
            statusCode = StatusCodes.Status400BadRequest;
        }

        // Adicione outras verificações de exceção conforme necessário

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var result = JsonSerializer.Serialize(new { error = exception.Message });
        return context.Response.WriteAsync(result);
    }
}

public static class ErrorHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}