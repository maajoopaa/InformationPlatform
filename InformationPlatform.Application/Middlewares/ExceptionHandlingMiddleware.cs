using System.Net;
using System.Text.Json;
using InformationPlatform.Application.Exceptions;

namespace InformationPlatform.Application.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, ex.Message);
            
            await HandleExceptionAsync(context, ex.Message,ex.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            
            await HandleExceptionAsync(context, ex.Message, 500);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, string errorMessage, int statusCode)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        response.StatusCode = statusCode;

        return response.WriteAsync(errorMessage);
    }
}