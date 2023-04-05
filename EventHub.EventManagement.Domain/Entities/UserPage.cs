namespace EventHub.EventManagement.Domain.Entities
{
   public class UserPage
   {
      public Guid UserPageId { get; set; }
      public PageType Type { get; set; }
      public Guid EntityId { get; set; }
   }

   public enum PageType
   {
      producer,
      organization
   }
}
