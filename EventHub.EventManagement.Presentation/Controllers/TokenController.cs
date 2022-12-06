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

      /// <summary>
      /// creates a new access token and refresh token
      /// </summary>
      /// <param name="tokenDto"></param>
      /// <returns></returns>
      [HttpPost("refresh")]
      [ProducesResponseType(200)]
      [ProducesResponseType(400)]
      public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
      {
         var tokenToReturn = await _serviceManager.AuthenticationService.RefreshToken(tokenDto);
         return Ok(tokenToReturn);
      }

   }
}
