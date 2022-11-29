using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.UserDto;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.EventManagement.Presentation.Controllers
{

   [Route("api/token")]
   [ApiController]
   public class TokenController : ControllerBase
   {
      private readonly IServiceManager _serviceManager;

      public TokenController(IServiceManager serviceManager)
      {
         _serviceManager = serviceManager;
      }

      [HttpPost("refresh")]
      public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
      {
         var tokenToReturn = await _serviceManager.AuthenticationService.RefreshToken(tokenDto);
         return Ok(tokenToReturn);
      }

   }
}
