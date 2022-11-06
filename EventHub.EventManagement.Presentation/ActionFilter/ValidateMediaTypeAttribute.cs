using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace EventHub.EventManagement.Presentation.ActionFilter
{
   public class ValidateMediaTypeAttribute : IActionFilter
   {
      public void OnActionExecuting(ActionExecutingContext context)
      {
         var mediaTypePresent = context.HttpContext.Request.Headers.ContainsKey("Accept");
         if (!mediaTypePresent)
         {
            context.Result = new BadRequestObjectResult("Accept header is missing.");
            return;
         }

         var mediaType = context.HttpContext.Request.Headers["Accept"].FirstOrDefault();
         if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue? value))
         {
            context.Result = new BadRequestObjectResult
               ("Media type not present, please add Accept header with the required media type.");
            return;
         }

         context.HttpContext.Items.Add("AcceptHeaderMediaType", value);
      }

      public void OnActionExecuted(ActionExecutedContext context)
      {
      }
   }
}
