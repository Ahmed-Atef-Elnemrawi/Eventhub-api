using EventHub.EventManagement.Domain.Entities;


namespace EventHub.EventManagement.Application.DTOs
{
   public class UserPageDto
   {
      public Guid EntityId { get; init; }
      public PageType PageType { get; init; }
   }
}
