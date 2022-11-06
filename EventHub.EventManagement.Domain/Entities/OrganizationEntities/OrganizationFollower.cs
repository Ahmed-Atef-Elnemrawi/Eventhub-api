namespace EventHub.EventManagement.Domain.Entities.OrganizationEntities
{
   public class OrganizationFollower
   {
      public Guid OrganizationId { get; set; }
      public Guid FollowerId { get; set; }
   }
}
