using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace VacationTrackerApi.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly LoggingOptions _loggingOptions;

        public LoggingMiddleware(
            RequestDelegate next,
            IOptions<LoggingOptions> loggingOptions,
            ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _loggingOptions = loggingOptions.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var logScope = _loggingOptions.GetLogScopeFunc?.Invoke(context);
            if (logScope == null || !logScope.Any())
            {
                await InvokeWithLoggingAsync();
                return;
            }

            using (_logger.BeginScope(logScope))
            {
                await InvokeWithLoggingAsync();
            }

            async Task InvokeWithLoggingAsync()
            {
                try
                {
                    _logger.LogInformation(
                        "Request {Method} {Url} starts",
                        context.Request?.Method,
                        context.Request?.Path.Value);
                    await _next(context);
                }
                finally
                {
                    _logger.LogInformation(
                        "Request {Method} {Url} ended with status code {StatusCode}",
                        context.Request?.Method,
                        context.Request?.Path.Value,
                        context.Response?.StatusCode);
                }
            }
        }
    }
}
