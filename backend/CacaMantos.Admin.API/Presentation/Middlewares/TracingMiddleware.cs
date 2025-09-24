namespace CacaMantos.Admin.API.Presentation.Middlewares
{
    public class TracingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TracingMiddleware> _logger;
        private const string CorrelationHeader = "X-Correlation-Id";

        public TracingMiddleware(RequestDelegate next, ILogger<TracingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request.Headers[CorrelationHeader].FirstOrDefault();

            if (string.IsNullOrEmpty(correlationId))
                correlationId = context.TraceIdentifier;

            context.Response.OnStarting(() =>
            {
                context.Response.Headers[CorrelationHeader] = correlationId;
                return Task.CompletedTask;
            });

            using (_logger.BeginScope(VariaveisTemplateLog(context, correlationId)))
            {
                await _next(context);
            }
        }

        private static Dictionary<string, object> VariaveisTemplateLog(HttpContext context, string correlationId)
        {
            return new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId,
                ["RequestId"] = context.TraceIdentifier
            };
        }
    }
}