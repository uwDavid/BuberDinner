using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuberDinner.Api.Middleware;

// Exception Handling Approach 1 - Global Middleware
public class ErrorHandlingMiddleware
{
    // property
    private readonly RequestDelegate _next;

    // constructor 
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // called by previous middlerware here via invoke?
    public async Task Invoke(HttpContext context)
    {
        try
        {
            // call next middlware
            await _next(context);
        }
        catch (Exception ex)
        {
            // handle exception
            await HandleExceptionAsync(context, ex);
        }
    }
    // method to handle exception
    public static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; // 500 error
        var result = JsonSerializer.Serialize(new { error = "Error occurred while processing request." });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}