using System.Collections;
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
        Dictionary<Type, int> exceptionStatusCodes = new Dictionary<Type, int>()
        {
            { typeof(NotFoundSectorException), StatusCodes.Status404NotFound },
            { typeof(NotFoundDoctorException), StatusCodes.Status404NotFound },
            { typeof(NotFoundPatientException), StatusCodes.Status404NotFound },
            { typeof(PatientArgumentsInvalidException), StatusCodes.Status400BadRequest },
            { typeof(InvalidArgumentsUpdateSectorException), StatusCodes.Status400BadRequest },
            { typeof(FailedToSetAppointmentOnPDFGeneratorException), StatusCodes.Status400BadRequest }
        };
        
        var statusCode = StatusCodes.Status500InternalServerError; // Internal Server Error por padrão
        if (exceptionStatusCodes.ContainsKey(exception.GetType()))
        {
            statusCode = exceptionStatusCodes[exception.GetType()];
        }

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