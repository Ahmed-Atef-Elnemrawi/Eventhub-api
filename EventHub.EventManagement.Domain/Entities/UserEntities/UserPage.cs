using EventHub.EventManagement.Domain.Common;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;

namespace EventHub.EventManagement.Domain.Entities.UserEntities
{
   public class UserPage : ISortableEntity, ISearchableEntity
   {
      public Guid UserPageId { get; set; }
      public string? PageName { get; set; }
      public Organization? Organization { get; set; }
      public Producer? Producer { get; set; }
   }
}
