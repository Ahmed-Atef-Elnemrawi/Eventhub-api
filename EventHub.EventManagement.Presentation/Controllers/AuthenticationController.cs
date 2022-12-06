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
      /// <summary>
      /// Provides user register
      /// </summary>
      /// <param name="userForRegistration"></param>
      /// <returns></returns>
      [HttpPost]
      [ProducesResponseType(201)]
      [ProducesResponseType(400)]
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

      /// <summary>
      /// provides user login
      /// </summary>
      /// <param name="userForAuthentication"></param>
      /// <returns></returns>
      [HttpPost("login")]
      [ProducesResponseType(200, Type = typeof(TokenDto))]
      [ProducesResponseType(400)]
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
