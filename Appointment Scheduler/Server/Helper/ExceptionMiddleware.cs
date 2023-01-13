using Models;

namespace Appointment_Scheduler.Server.Helper
{
    public class ExceptionMiddleware
    {
        
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger)
        {
            
            _requestDelegate=requestDelegate;
            _logger=logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Something definitely went wrong");
                await HandleRequestError(ex, context);
            }
        }

        private async Task HandleRequestError(Exception ex, HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            _logger.LogError(ex, "Error Handled in Middleware");

            await context.Response.WriteAsync(new ErrorModel
            {
                ErrorCode = context.Response.StatusCode,
                ErrorDesc = "internal server error"
            }.ToString());

        }
    }
}
