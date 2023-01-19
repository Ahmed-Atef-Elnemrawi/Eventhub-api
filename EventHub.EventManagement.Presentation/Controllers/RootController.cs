using EventHub.EventManagement.Application.Models.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EventHub.EventManagement.Presentation.Controllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiversion}")]
   [ApiController]
   public class RootController : ControllerBase
   {
      private readonly LinkGenerator _linkGenerator;

      public RootController(LinkGenerator linkGenerator)
      {
         _linkGenerator = linkGenerator;
      }

      /// <summary>
      /// Gets the starting point of our EventHub API.
      /// </summary>
      /// <param name="mediaType"></param>
      /// <returns></returns>
      [HttpGet(Name = "GetRoot")]
      [ProducesResponseType(200, Type = typeof(IEnumerable<Link>))]
      [ProducesResponseType(204)]
      [ProducesResponseType(404)]
      public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType)
      {
         if (mediaType.Contains("application/vnd.api.apiroot"))
         {
            var list = new List<Link>
         {
            new Link(_linkGenerator.GetUriByName(HttpContext,nameof(GetRoot),new{}),
            rel:"self", method:"GET"),

            new Link(_linkGenerator.GetUriByName(HttpContext,"GetOrganizations",new{}),
            rel:"organizations", method:"GET"),

            new Link(_linkGenerator.GetUriByName(HttpContext,"CreateOrganization",new{}),
            rel:"create-organization", method:"POST"),

            new Link(_linkGenerator.GetUriByName(HttpContext,"GetProducers",new{}),
            rel:"producers", method:"GET"),

            new Link(_linkGenerator.GetUriByName(HttpContext,"CreateProducer",new{}),
            rel:"create-producer", method:"POST"),

            new Link(_linkGenerator.GetUriByName(HttpContext,"GetEvents",new{}),
            rel:"events", method:"GET"),

             new Link(_linkGenerator.GetUriByName(HttpContext,"GetMediums",new{}),
             rel:"mediums", method:"GET"),

             new Link(_linkGenerator.GetUriByName(HttpContext,"Register", new{}),
             rel:"register-user", method:"POST"),

             new Link(_linkGenerator.GetUriByName(HttpContext, "Login   ", new{}),
             rel:"login-user", method:"POST"),
         };

            return Ok(list);
         }

         return NoContent();
      }

   }
}
