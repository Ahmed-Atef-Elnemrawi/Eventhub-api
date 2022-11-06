using EventHub.EventManagement.Domain.Entities.EventEntities;

namespace EventHub.EventManagement.Application.RequestFeatures.Params
{
   public class EventParams : RequestParameters
   {
      public string? Category { get; set; } = string.Empty;
      public MediumType MediumType { get; set; } = MediumType.None;
      public bool UpComing { get; set; } = false;
      public bool Last24Hours { get; set; } = false;
      public bool LastWeek { get; set; } = false;
      public bool LastMonth { get; set; } = false;
      public int Year { get; set; } = DateTime.Now.Year;

   }
}
