namespace EventHub.EventManagement.Application.Models.LinkModels
{
   public class LinkResponse
   {
      public bool HasLinks { get; set; }
      public List<ShapedEntity>? ShapedEntities { get; set; }
      public LinkCollectionWrapper<Entity>? LinkedEntities { get; set; }
      public ShapedEntity? LinkedEntity { get; set; }
      public ShapedEntity? ShapedEntity { get; set; }

      public LinkResponse()
      {
         ShapedEntities = new List<ShapedEntity>();
         LinkedEntities = new LinkCollectionWrapper<Entity>();
      }
   }
}
