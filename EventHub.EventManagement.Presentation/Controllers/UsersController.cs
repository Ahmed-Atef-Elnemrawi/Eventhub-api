using EventHub.EventManagement.Application.Contracts.Infrastructure;
using EventHub.EventManagement.Application.Contracts.links;
using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs;
using EventHub.EventManagement.Application.DTOs.UserDto;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Validation;
using EventHub.EventManagement.Domain.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace EventHub.EventManagement.Presentation.Controllers
{
   [ApiVersion("1.0")]
   [Route("api/v{v:apiVersion}/users")]
   [ApiController]
   public class UsersController : ControllerBase
   {
      private readonly IServiceManager _service;
      private readonly IEmailSender _emailSender;
      private readonly UserManager<User> _userManager;

      public UsersController(IServiceManager service, IEmailSender emailSender, UserManager<User> userManager, IEntitiesLinkGeneratorManager linkGeneratorManager)
      {
         _service = service;
         _emailSender = emailSender;
         _userManager = userManager;
      }


      [HttpGet("{id}", Name = "GetUser")]
      [ProducesResponseType(200, Type = typeof(UserProfileDto))]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      public async Task<IActionResult> GetUser([FromRoute] string id)
      {
         if (string.IsNullOrEmpty(id))
            return BadRequest();

         var user = await _userManager.FindByIdAsync(id);
         if (user is null)
            return NotFound();

         var userToReturn = new UserProfileDto
         {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            LiveIn = user.LiveIn,
            Age = user.Age,
            Genre = user.Genre,
            PhoneNumber = user.PhoneNumber,
            UserPageId = user.UserPageId,
         };


         return Ok(userToReturn);
      }


      /// <summary>
      /// Provides signup function
      /// </summary>
      /// <param name="userForRegistration"></param>
      /// <returns></returns>
      [HttpPost("signup", Name = "SignUp")]
      [ProducesResponseType(201)]
      [ProducesResponseType(400)]
      public async Task<IActionResult> Register([FromBody] UserForRegistrationDto userForRegistration)
      {
         var validationResult = await new UserValidator().ValidateAsync(userForRegistration);
         if (!validationResult.IsValid)
         {
            validationResult.AddToModelState(ModelState);
            return BadRequest(ModelState);
         }

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
      /// Provides login function
      /// </summary>
      /// <param name="userForAuthentication"></param>
      /// <returns></returns>
      [HttpPost("login", Name = "Login")]
      [ProducesResponseType(200, Type = typeof(TokenDto))]
      [ProducesResponseType(401)]
      public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
      {

         var valideUser = await _service.AuthenticationService.ValidateUser(userForAuthentication);

         if (!valideUser)
            return Unauthorized();

         var tokenDto = await _service
            .AuthenticationService.CreateToken(populateExp: true);


         return Ok(tokenDto);
      }

      /// <summary>
      /// Provide forgot password function 'emailing user with reset-token + client-redirect-url'
      /// </summary>
      /// <param name="forgotPasswordDto"></param>
      /// <returns></returns>
      [HttpPost("forgotPassword", Name = "ForgotPassword")]
      [ProducesResponseType(200)]
      [ProducesResponseType(400)]
      public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email!);
         if (user is null)
         {
            return BadRequest();
         }

         var token = await _userManager.GeneratePasswordResetTokenAsync(user);
         var param = new Dictionary<string, string?>
         {
            {"token", token },
            {"email", forgotPasswordDto.Email }
         };
         var redirectURI =
            QueryHelpers.AddQueryString(forgotPasswordDto.ResetPasswordClientURI!, param);

         var email =
            new Email(new List<string> { forgotPasswordDto.Email! }, "reset password token", redirectURI);

         await _emailSender.SendEmailAsync(email);
         return Ok();
      }

      /// <summary>
      /// Provide reset password funtion using reset-token which emailed to the user
      /// </summary>
      /// <param name="resetPasswordDto"></param>
      /// <returns></returns>
      [HttpPost("resetPassword", Name = "ResetPassword")]
      [ProducesResponseType(200)]
      [ProducesResponseType(400)]
      public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
      {
         if (!ModelState.IsValid)
            return BadRequest();

         var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email!);
         if (user is null)
            return BadRequest();

         var resetPassword = await _userManager
            .ResetPasswordAsync(user, resetPasswordDto.Token!, resetPasswordDto.Password!);

         if (!resetPassword.Succeeded)
         {
            var errors = resetPassword.Errors.Select(err => err.Description);
            return BadRequest(new { errors });
         }

         return Ok();
      }



      /// <summary>
      /// Provide update user function
      /// </summary>
      /// <param name="id"></param>
      /// <param name="user"></param>
      /// <returns></returns>
      [HttpPut("{id}", Name = "UpdateUser")]
      [ProducesResponseType(200)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UserProfileForUpdateDto user)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         var userEntity = await _userManager.FindByIdAsync(id);

         if (userEntity is null)
            return NotFound();

         userEntity.FirstName = user.FirstName;
         userEntity.LastName = user.LastName;
         userEntity.UserName = user.UserName;
         userEntity.Email = user.Email;
         userEntity.PhoneNumber = user.PhoneNumber;
         userEntity.Genre = user.Genre;
         userEntity.Age = user.Age;
         userEntity.LiveIn = user.LiveIn;
         userEntity.ProfilePicture = user.ProfilePicture;
         userEntity.UserPage = user.UserPage;

         await _userManager.UpdateAsync(userEntity);
         return NoContent();

      }

      /// <summary>
      /// Provide change user password function
      /// </summary>
      /// <param name="id"></param>
      /// <param name="changePasswordDto"></param>
      /// <returns></returns>
      [HttpPut("{id}/changePassword", Name = "ChangePassword")]
      [ProducesResponseType(200)]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      public async Task<IActionResult> ChangePassword(
         [FromRoute] string id,
         [FromBody] ChangePasswordDto changePasswordDto)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         var userEntity = await _userManager.FindByIdAsync(id);
         if (userEntity is null)
            return NotFound();

         var result = await _userManager.ChangePasswordAsync(
            userEntity,
            changePasswordDto.CurrentPassword!,
            changePasswordDto.NewPassword!);

         if (!result.Succeeded)
         {
            foreach (var error in result.Errors)
            {
               ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
         }
         return NoContent();
      }


      [HttpDelete("{id}", Name = "RemoveUser")]
      [ProducesResponseType(204)]
      [ProducesResponseType(404)]
      public async Task<IActionResult> RemoveUser(string id)
      {
         var user = await _userManager.FindByIdAsync(id);
         if (user is null)
            return NotFound();

         await _userManager.DeleteAsync(user);
         return NoContent();
      }
   }
}
