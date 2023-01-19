using EventHub.EventManagement.Application.Contracts.Infrastructure;
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
   [Route("api/users")]
   [ApiController]
   public class UsersController : ControllerBase
   {
      private readonly IServiceManager _service;
      private readonly IEmailSender _emailSender;
      private readonly UserManager<User> _userManager;

      public UsersController(IServiceManager service, IEmailSender emailSender, UserManager<User> userManager)
      {
         _service = service;
         _emailSender = emailSender;
         _userManager = userManager;
      }
      /// <summary>
      /// Provides signup function
      /// </summary>
      /// <param name="userForRegistration"></param>
      /// <returns></returns>
      [HttpPost(Name = "Register")]
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
      [ProducesResponseType(200, Type = typeof(AuthResponseDto))]
      [ProducesResponseType(401)]
      public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
      {

         var valideUser = await _service.AuthenticationService.ValidateUser(userForAuthentication);

         if (!valideUser)
            return Unauthorized();

         var authResponse = await _service.AuthenticationService.CreateAuthResponse(populateTokenExpiration: true);

         return Ok(authResponse);
      }

      /// <summary>
      /// Provide forgot password function 'emailing user with reset-token + client-redirect-url'
      /// </summary>
      /// <param name="forgotPasswordDto"></param>
      /// <returns></returns>
      [HttpPost("ForgotPassword")]
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
      [HttpPost("ResetPassword")]
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
      /// <param name="userName"></param>
      /// <param name="user"></param>
      /// <returns></returns>
      [HttpPut("{userName}")]
      public async Task<IActionResult> UpdateUser([FromRoute] string userName, [FromBody] UserProfileDto user)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         var userEntity = await _userManager.FindByNameAsync(userName);

         if (userEntity is null)
            return NotFound();

         userEntity.FirstName = user.FirstName;
         userEntity.LastName = user.LastName;
         userEntity.UserName = user.UserName;
         userEntity.Email = user.Email;
         userEntity.PhoneNumber = user.PhoneNumber;
         userEntity.Genre = user.Genre;
         userEntity.Age = user.Age;
         userEntity.LiveIn = user.Country;
         userEntity.ProfilePicture = user.ProfilePicture;

         await _userManager.UpdateAsync(userEntity);
         return NoContent();

      }

      /// <summary>
      /// Provide change user password function
      /// </summary>
      /// <param name="userName"></param>
      /// <param name="changePasswordDto"></param>
      /// <returns></returns>
      [HttpPut("{userName}/changePassword", Name = "ChangePassword")]
      public async Task<IActionResult> ChangePassword([FromRoute] string userName, [FromBody] ChangePasswordDto changePasswordDto)
      {
         if (!ModelState.IsValid)
            return BadRequest(ModelState);

         var userEntity = await _userManager.FindByNameAsync(userName);
         if (userEntity is null)
            return NotFound();

         var result = await _userManager.ChangePasswordAsync(userEntity, changePasswordDto.CurrentPassword!, changePasswordDto.NewPassword!);

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
   }
}
