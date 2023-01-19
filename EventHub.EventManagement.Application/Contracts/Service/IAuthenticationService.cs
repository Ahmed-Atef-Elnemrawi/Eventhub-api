using EventHub.EventManagement.Application.DTOs.UserDto;
using Microsoft.AspNetCore.Identity;

namespace EventHub.EventManagement.Application.Contracts.Service;

public interface IAuthenticationService
{
   Task<IdentityResult> RegisterUser(UserForRegistrationDto userForCreation);
   Task<bool> ValidateUser(UserForAuthenticationDto userForAuthentication);
   Task<TokenDto> CreateToken(bool populateExp);
   Task<TokenDto> RefreshToken(TokenDto tokenDto);
   Task<AuthResponseDto> CreateAuthResponse(bool populateTokenExpiration);
}
