using EventHub.EventManagement.Application.Contracts.Service;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.EventManagement.Presentation.Controllers
{
   [Route("api/v1.0/userpages")]
   [ApiController]
   public class UserPageController : ControllerBase
   {
      private readonly IServiceManager _service;

      public UserPageController(IServiceManager service)
      {
         _service = service;
      }

      [HttpGet("{id:guid}", Name = "GetUserPage")]
      public async Task<IActionResult> GetUserPageAsync([FromRoute] Guid id)
      {
         if (!ModelState.IsValid)
            return NotFound(ModelState);

         var userPage = await _service.UserPageService
              .GetUserPageAsync(id, trackChanges: false);
         return Ok(userPage);

      }
   }
}
