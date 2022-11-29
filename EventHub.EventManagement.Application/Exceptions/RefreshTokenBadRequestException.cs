namespace EventHub.EventManagement.Application.Exceptions
{
   public sealed class RefreshTokenBadRequest : BadRequestException
   {
      public RefreshTokenBadRequest()
         : base("Invalid Client Request. The TokenDto Has Some Invalid Values.")
      {
      }
   }
}
