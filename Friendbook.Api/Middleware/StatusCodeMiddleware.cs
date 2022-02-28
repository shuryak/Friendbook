using Newtonsoft.Json;

namespace Friendbook.Api.Middleware;

public class StatusCodeMiddleware
{
    private readonly RequestDelegate _next;

    public StatusCodeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        await _next.Invoke(httpContext);
        
        if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            httpContext.Response.ContentType = "application/json";
            
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                message = "Unauthorized"
            }));
        }
    }
}

public static class StatusCodeMiddlewareExtensions
{
    public static void StatusCodeMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<StatusCodeMiddleware>();
    }
}
