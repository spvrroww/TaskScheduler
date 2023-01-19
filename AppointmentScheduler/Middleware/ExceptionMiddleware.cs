using Microsoft.AspNetCore.Diagnostics;
using Models;

namespace AppointmentScheduler.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate requestDelegate)
        {
            _logger=logger;
            _requestDelegate= requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleRequestError(ex, context);
            }
        }

        public async Task HandleRequestError(Exception ex, HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType ="application/json";

            _logger.LogError(ex, "Error Handled in Middleware");
            context.Request.Path = "/Home/Error";
            context.Response.Redirect(context.Request.Path);
            //await context.Response.WriteAsync(new ErrorModel
            //{
            //    ErrorCode = context.Response.StatusCode,
            //    ErrorDesc = "internal server error"
            //}.ToString());

         
           
            
        }


    }
}
