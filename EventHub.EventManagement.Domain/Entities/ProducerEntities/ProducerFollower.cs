namespace EventHub.EventManagement.Domain.Entities.ProducerEntities
{
   public class ProducerFollower
   {
      public Guid ProducerId { get; set; }
      public Guid FollowerId { get; set; }
   }
}
