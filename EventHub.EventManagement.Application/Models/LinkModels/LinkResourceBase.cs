namespace EventHub.EventManagement.Application.Models.LinkModels
{
   public class LinkResourceBase
   {
      public LinkResourceBase() { }

      public List<Link>? Links { get; set; } = new();
   }
}
