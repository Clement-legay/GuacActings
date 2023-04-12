using System.Net;
using System.Text.Json;

namespace guacactings.Middleware;

public class ErrorHandlingMiddleware
{
    #region Fields

    private readonly RequestDelegate _next;

    #endregion

    #region Constructor

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    #endregion

    #region Methods

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = ex.Message }));
        }
    }

    #endregion
}

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}