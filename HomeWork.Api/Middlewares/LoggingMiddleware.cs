
using System.Diagnostics;

namespace HomeWork.Api.Middlewares
{
    public class LoggingMiddleware : IMiddleware
    {
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var method = context.Request.Method;
            var path = context.Request.Path;

            _logger.LogInformation($"-> Request: {method} {path}");

            var sw = Stopwatch.StartNew();
            await next(context);
            sw.Stop();

            _logger.LogInformation($"<- response: {context.Response.StatusCode} {sw.ElapsedMilliseconds}ms");
        }
    }
}
