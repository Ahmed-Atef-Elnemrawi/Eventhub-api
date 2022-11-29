using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.UserDto;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.EventManagement.Presentation.Controllers
{
   [Route("api/authentication")]
   [ApiController]
   public class AuthenticationController : ControllerBase
   {
      private readonly IServiceManager _service;

      public AuthenticationController(IServiceManager service)
      {
         _service = service;
      }

      [HttpPost]
      public async Task<IActionResult> Register([FromBody] UserForRegistrationDto userForRegistration)
      {
         var result = await _service.AuthenticationService.RegisterUser(userForRegistration);

         if (!result.Succeeded)
         {
            foreach (var error in result.Errors)
               ModelState.TryAddModelError(error.Code, error.Description);
            return BadRequest(ModelState);
         }

         return StatusCode(201);
      }

      [HttpPost("login")]
      public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
      {
         var valideUser = await _service.AuthenticationService.ValidateUser(userForAuthentication);

         if (!valideUser)
            return Unauthorized();

         var tokenDto = await _service.AuthenticationService.CreateToken(populateExp: true);
         return Ok(tokenDto);

      }


   }
}
