using System.Text.Json;

namespace UserManagementAPI.MiddlewareTrackers;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private const string ValidToken = "TechHive123";

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var token))
        {
            await ReturnUnauthorized(context);
            return;
        }

        if (token != ValidToken)
        {
            await ReturnUnauthorized(context);
            return;
        }

        await _next(context);
    }

    private static async Task ReturnUnauthorized(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/json";

        var response = JsonSerializer.Serialize(new
        {
            error = "Unauthorized. Invalid token."
        });

        await context.Response.WriteAsync(response);
    }
}