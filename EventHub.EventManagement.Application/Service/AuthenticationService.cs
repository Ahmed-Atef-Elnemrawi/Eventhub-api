using AutoMapper;
using EventHub.EventManagement.Application.Contracts.Infrastructure;
using EventHub.EventManagement.Application.Contracts.Service;
using EventHub.EventManagement.Application.DTOs.UserDto;
using EventHub.EventManagement.Application.Exceptions;
using EventHub.EventManagement.Application.Models.ConfigurationModels;
using EventHub.EventManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EventHub.EventManagement.Application.Service
{
   internal sealed class AuthenticationService : IAuthenticationService
   {
      private readonly UserManager<User> _userManager;
      private readonly ILoggerManager _logger;
      private readonly IMapper _mapper;
      private readonly JwtConfiguration _jwtConfiguration;
      private User? _user;

      public AuthenticationService(UserManager<User> userManager,
         ILoggerManager logger, IMapper mapper, IOptions<JwtConfiguration> jwtConfiguration)
      {
         _userManager = userManager;
         _logger = logger;
         _mapper = mapper;
         _jwtConfiguration = jwtConfiguration.Value;
      }

      public async Task<TokenDto> CreateToken(bool populateExp)
      {
         var claims = await GetClaims();
         var singingCredentials = GetSigningCredentials();
         var securityToken = GenerateSecurityToken(claims, singingCredentials);
         var refreshToken = GenerateRefreshToken();
         _user!.RefreshToken = refreshToken;

         if (populateExp)
            _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);

         await _userManager.UpdateAsync(_user);

         var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

         return new TokenDto(accessToken, refreshToken);
      }

      private SigningCredentials GetSigningCredentials()
      {
         var secret = Environment.GetEnvironmentVariable("secret");

         var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
         return new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
      }

      private async Task<List<Claim>> GetClaims()
      {
         var claims = new List<Claim>
         {
            new Claim(ClaimTypes.Name, _user!.UserName),

         };


         foreach (var role in await _userManager.GetRolesAsync(_user))
            claims.Add(new Claim(ClaimTypes.Role, role));

         return claims;
      }

      private JwtSecurityToken GenerateSecurityToken(List<Claim> claims, SigningCredentials signingCredentials)
      {
         return new JwtSecurityToken(
            issuer: _jwtConfiguration.ValidIssuer,
            audience: _jwtConfiguration.ValidAudience,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.Expires)),
            claims: claims,
            signingCredentials: signingCredentials);
      }

      public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForCreation)
      {
         var user = _mapper.Map<User>(userForCreation);
         var result = await _userManager.CreateAsync(user, userForCreation.Password);

         if (result.Succeeded)
         {
            await _userManager.AddToRolesAsync(user, userForCreation.Roles);
         }
         return result;
      }

      public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthentication)
      {
         _user = await _userManager.FindByEmailAsync(userForAuthentication.Email);
         var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuthentication.Password));

         if (!result)
            _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Worng user name or password.");

         return result;
      }

      private string GenerateRefreshToken()
      {
         var randomNumber = new Byte[32];

         using var rng = RandomNumberGenerator.Create();
         rng.GetBytes(randomNumber);
         return Convert.ToBase64String(randomNumber);
      }


      private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
      {
         var tokenValidationParameters = new TokenValidationParameters
         {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = _jwtConfiguration.ValidIssuer,
            ValidAudience = _jwtConfiguration.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("secret")!))
         };

         var tokenHandler = new JwtSecurityTokenHandler();

         var principal = tokenHandler
             .ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

         var jwtSecurityToken = securityToken as JwtSecurityToken;

         if (jwtSecurityToken is null ||
            !jwtSecurityToken.Header.Alg
            .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
         {
            throw new SecurityTokenException("invalid token");
         }

         return principal;
      }

      public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
      {
         var principal = GetPrincipalFromExpiredToken(tokenDto.AcessToken);
         var user = await _userManager.FindByNameAsync(principal.Identity!.Name);

         if (user is null ||
            user.RefreshToken != tokenDto.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new RefreshTokenBadRequest();

         _user = user;
         return await CreateToken(populateExp: false);
      }
   }
}
