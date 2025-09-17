
namespace HomeWork.Api.Middlewares
{
    public class RoleMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var path = context.Request.Path.Value!;

            if (path.StartsWith("/swagger") || path.StartsWith("/openapi") || path.StartsWith("/styles"))
            {
                await next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("X-User-Role", out var role))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden: Missing role");
                return;
            }

            if (role == "Admin")
            {
                await next(context);
            }
            else if (role == "User" && context.Request.Method == HttpMethods.Get)
            {
                await next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden: Access denied");
            }
        }
    }
}
