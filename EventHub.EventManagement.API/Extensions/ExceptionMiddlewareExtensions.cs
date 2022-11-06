using EventHub.EventManagement.Application.Contracts.Logging;
using EventHub.EventManagement.Application.ErrorModels;
using EventHub.EventManagement.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace EventHub.EventManagement.API.Extensions
{
   public static class ExceptionMiddlewareExtensions
   {
      public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
      {
         app.UseExceptionHandler(builder => builder.Run(
            async context =>
            {
               context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
               context.Response.ContentType = "application/json";

               var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
               if (exceptionHandlerFeature != null)
               {
                  context.Response.StatusCode = exceptionHandlerFeature.Error switch
                  {
                     NotFoundException => (int)HttpStatusCode.NotFound,
                     _ => (int)HttpStatusCode.InternalServerError
                  };

                  await context.Response.WriteAsync(new ErrorDetails()
                  {
                     StatusCode = context.Response.StatusCode,
                     Message = exceptionHandlerFeature.Error.Message,
                  }.ToString());
               }
            }));
      }
   }
}
