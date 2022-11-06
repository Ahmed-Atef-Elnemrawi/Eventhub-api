

namespace EventHub.EventManagement.Domain.Entities.EventEntities
{
   public class Medium
   {
      public Guid MediumId { get; set; }
      public MediumType Type { get; set; }
      public ICollection<Category> Categories { get; set; }

      public Medium()
      {
         Categories = new List<Category>();
      }
   }


   public enum MediumType
   {
      None,
      Live,
      Virtual,
      Hybird
   }

}
