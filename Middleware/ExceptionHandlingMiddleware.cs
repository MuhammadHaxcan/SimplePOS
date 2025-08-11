using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace SimplePOS.Middleware
{

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                if (context.Request.Headers["Accept"].ToString().Contains("application/json") ||
                    context.Request.Path.StartsWithSegments("/api") ||
                    context.Request.ContentType == "application/json")
                {
                    context.Response.ContentType = "application/json";
                    var payload = new { error = "An unexpected error occurred." };
                    await context.Response.WriteAsJsonAsync(payload);
                }
                else
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync("<html><body><h1>Something went wrong.</h1><p>Please contact support.</p></body></html>");
                }
            }
        }
    }

}
