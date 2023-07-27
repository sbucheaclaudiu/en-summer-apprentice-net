using System.Net;
using Newtonsoft.Json;
using WebApplication1.Exceptions;
using WebApplication1.Models.DTO;

namespace WebApplication1.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _nextRequestDelegate;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _nextRequestDelegate = requestDelegate;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _nextRequestDelegate(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";
        string message;

        switch (exception)
        {
            case EntityNotFoundException ex:
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                message = ex.Message;
                break;
            
            case ArgumentNullException ex:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                message = ex.Message;
                break;
            
            default:
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                message = "Internal service error!";
                break;
        }
        
        _logger.LogError(exception.Message, exception.StackTrace);
        
        var result = JsonConvert.SerializeObject(new { errorMessage = message });
        await httpContext.Response.WriteAsync(result);
    }
}