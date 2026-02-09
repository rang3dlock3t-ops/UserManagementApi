namespace UserManagementApi.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _logger= logger;_next= next;
             
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Incoming request: {method} {path}", context.Request.Method, context.Request.Path);
            await _next(context);
            _logger.LogInformation("Outgoing reponse: {statusCode}", context.Response.StatusCode);
        }
    }
}
