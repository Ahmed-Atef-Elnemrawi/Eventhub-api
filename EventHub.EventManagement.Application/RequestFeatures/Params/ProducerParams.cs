namespace EventHub.EventManagement.Application.RequestFeatures.Params
{
   public class ProducerParams : RequestParameters
   {
      public string? JobTitle { get; set; }
      public bool Latest { get; set; }

   }
}
