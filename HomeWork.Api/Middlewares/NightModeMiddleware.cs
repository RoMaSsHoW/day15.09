
namespace HomeWork.Api.Middlewares
{
    public class NightModeMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var now = DateTime.Now;
            if (now.Hour >= 0 && now.Hour < 6)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Сервис недоступен ночью (00:00 - 06:00)");
                return;
            }

            await next(context);
        }
    }
}
