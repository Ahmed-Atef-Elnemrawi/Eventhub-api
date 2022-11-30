namespace EventHub.EventManagement.Application.Models.ConfigurationModels
{
   public class JwtConfiguration
   {
      public string? Section => "JwtSettings";
      public string? ValidAudience { get; set; }
      public string? ValidIssuer { get; set; }
      public int Expires { get; set; }
   }
}
