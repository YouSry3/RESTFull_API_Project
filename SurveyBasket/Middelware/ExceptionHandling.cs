using Microsoft.AspNetCore.Mvc;

namespace SurveyBasket.Middelware
{
    public class ExceptionHandling(RequestDelegate next, ILogger<ExceptionHandling> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandling> _logger = logger;


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred while processing the request.", ex.Message);
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                };
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }

    }
}
