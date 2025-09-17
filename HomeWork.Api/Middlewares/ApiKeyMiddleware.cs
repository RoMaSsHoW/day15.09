
namespace HomeWork.Api.Middlewares
{
    public class ApiKeyMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var path = context.Request.Path.Value!;

            if (path.StartsWith("/swagger") || path.StartsWith("/openapi") || path.StartsWith("/styles"))
            {
                await next(context);
                return;
            }

            if (!context.Request.Headers.ContainsKey("X-API-KEY"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("❌ Unauthorized: Missing API Key");
                return;
            }

            await next(context);
        }
    }
}
