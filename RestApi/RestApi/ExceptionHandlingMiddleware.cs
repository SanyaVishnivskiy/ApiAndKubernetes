using RestApi.Domain.Exceptions;
using System.Text.Json;

namespace RestApi;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<ExceptionHandlingMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            await HandleExceptionAsync(context, e);
        }
    }
    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";
        await FillResponse(httpContext, exception);
    }

    private static Task FillResponse(HttpContext context, Exception exception)
    {
        if (exception is EntityNotFoundException)
        {
            return CreateResponse(context, StatusCodes.Status404NotFound, exception.Message);
        }

        if (exception is EntityValidationException validationException)
        {
            return CreateValidationErrorResponse(context, validationException);
        }

        if (exception is DuplicatedEntityException duplicationException)
        {
            return CreateResponse(context, StatusCodes.Status400BadRequest, duplicationException.Message);
        }

        return CreateResponse(context, StatusCodes.Status500InternalServerError, "InternalServerError");
    }

    private static async Task CreateResponse(HttpContext httpContext, int statusCode, string responseMessage)
    {
        var response = new
        {
            message = responseMessage,
            statusCode = statusCode
        };
        await WriteResponse(httpContext, statusCode, response);
    }

    private static async Task CreateValidationErrorResponse(HttpContext context, EntityValidationException exception)
    {
        var response = new
        {
            message = exception.Message,
            statusCode = StatusCodes.Status400BadRequest,
            errors = exception.Errors
        };
        await WriteResponse(context, response.statusCode, response);
    }

    private static async Task WriteResponse(HttpContext httpContext, int statusCode, object response)
    {
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
