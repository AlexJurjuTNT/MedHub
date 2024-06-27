using System.ComponentModel.DataAnnotations;
using Medhub_Backend.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using IExceptionHandler = Microsoft.AspNetCore.Diagnostics.IExceptionHandler;

namespace MedHub_Backend.WebApi;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }


    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, $"Exception occured: {exception.Message}");

        var problemDetails = new ProblemDetails
        {
            Status = DetermineStatusCode(exception),
            Title = exception.GetType().Name,
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }

    private static int DetermineStatusCode(Exception exception)
    {
        return exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status400BadRequest,
            
            UserNotFoundException => StatusCodes.Status404NotFound,
            UserAlreadyExistsException => StatusCodes.Status409Conflict,
            ClinicNotFoundException => StatusCodes.Status404NotFound,
            PasswordMismatchException => StatusCodes.Status400BadRequest,

            ArgumentException => StatusCodes.Status400BadRequest,
            InvalidOperationException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}