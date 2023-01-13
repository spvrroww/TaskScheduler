using Microsoft.AspNetCore.Diagnostics;
using Models;

namespace Appointment_Scheduler.Server.Helper
{
    public static class ExceptionMiddlewareExtension
    {
   
        public static void ConfigureExceptionHandler(this WebApplication app, ILogger logger)
        {
            
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var features = context.Features.Get<IExceptionHandlerFeature>();
                    

                    if (features != null)
                    {
                        logger.LogError(features.Error, "Something Definitely went wrong somewhere");

                        await context.Response.WriteAsync(new ErrorModel { ErrorCode = context.Response.StatusCode, ErrorDesc ="Internal Server Error" }.ToString());
                    }
                });
            });
        }
    }
}
