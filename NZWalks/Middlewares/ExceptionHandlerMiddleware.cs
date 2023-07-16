using System.Net;

namespace NZWalks.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);

            } catch (Exception ex)
            {
                var errorID = Guid.NewGuid();

                logger.LogError(ex, $"{errorID} : {ex.Message}");
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var err = new
                {
                    Id = errorID,
                    ErrorMessage = "Something went wrong!"
                };
                await httpContext.Response.WriteAsJsonAsync( err );

            }
        }
    }
}
