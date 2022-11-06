using EventHub.EventManagement.Application.Contracts.Links;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.Service.DataShaper;
using Microsoft.Net.Http.Headers;
using System.Reflection;

namespace EventHub.EventManagement.API.Utility
{
   public abstract class EntityLinkes<T> : IEntityLinks<T>
      where T : class
   {
      protected internal readonly LinkGenerator _linkGenerator;

      public EntityLinkes(LinkGenerator linkGenerator)
      {
         _linkGenerator = linkGenerator;
      }

      public LinkResponse TryGetEntitiesLinks(IEnumerable<T> entities,
                                              string fields,
                                              HttpContext httpContext,
                                              Guid? parentId,
                                              Guid? parentParentId)
      {
         var shapedEntities = ShapeEntities(entities, fields);

         if (ShouldReturnLinks(httpContext))
            return ReturnLinkedEntities(entities, parentId, parentParentId, fields, shapedEntities, httpContext);

         return ReturnShapedEntity(entities, fields);
      }


      public LinkResponse TryGetEntityLinks(T entityDto,
                                            string fields,
                                            HttpContext httpContext,
                                            Guid? parentId = null,
                                            Guid? parentParentId = null)
      {
         var linkResponse = new LinkResponse();
         var shapedEntity = ShapeEntity(entityDto, fields);

         if (ShouldReturnLinks(httpContext))
         {
            var idPropertyName = typeof(T).Name.TrimEnd('D', 't', 'o');
            var id = GetEntityId(entityDto, idPropertyName);
            var links = GenerateEntityLinks(httpContext, id, parentId, parentParentId, "");

            shapedEntity.Entity.Add("Links", links);
            linkResponse.HasLinks = true;
            linkResponse.LinkedEntity = shapedEntity;
         }
         else
            linkResponse.ShapedEntity = shapedEntity;

         return linkResponse;
      }

      private ShapedEntity ShapeEntity(T entityDto, string fields)
      {

         return new DataShaper<T>().ShapeData(entityDto, fields);

      }

      private List<Entity> ShapeEntities(IEnumerable<T> entities, string fields)
      {
         var shapedData = new DataShaper<T>().ShapeData(entities, fields);
         return shapedData.Select(s => s.Entity).ToList();

      }

      private bool ShouldReturnLinks(HttpContext httpContext)
      {
         var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"]!;

         return mediaType.SubTypeWithoutSuffix
            .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
      }

      private LinkResponse ReturnLinkedEntities(IEnumerable<T> entities,
                                                Guid? parentId,
                                                Guid? parentParentId,
                                                string fields,
                                                List<Entity> shapedEntities,
                                                HttpContext httpContext)
      {
         var entitiesList = entities.ToList();

         for (int index = 0; index < entitiesList.Count(); index++)
         {
            var idPropertyName = typeof(T).Name.TrimEnd('D', 't', 'o');
            var id = GetEntityId(entitiesList[index], idPropertyName);
            var entitiesLinks = GenerateEntityLinks(httpContext, id, parentId, parentParentId, fields);

            shapedEntities[index].Add("Links", entitiesLinks);
         }

         var entitiesCollection = new LinkCollectionWrapper<Entity>(shapedEntities);
         var LinkedEntities = GenerateEntitiesLinks(httpContext, entitiesCollection);

         return new LinkResponse { HasLinks = true, LinkedEntities = LinkedEntities };
      }


      internal virtual List<Link> GenerateEntityLinks(HttpContext httpContext,
                                                      Guid id,
                                                      Guid? parentId,
                                                      Guid? parentParentId,
                                                      string fields)
      {
         var links = new List<Link>
         {
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetEntities", values: new {id, fields}),
            rel: "self", method:"GET"),

            new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateEntities", values: new {id}),
            rel: "update-entities", method: "UPDATE"),

            new Link(_linkGenerator.GetUriByAction(httpContext, "RemoveEntities", values: new {id}),
            rel: "delete-entities", method: "DELETE"),
         };

         return links;
      }


      internal virtual LinkCollectionWrapper<Entity> GenerateEntitiesLinks
         (HttpContext httpContext, LinkCollectionWrapper<Entity> entitiesCollection)
      {
         var links = new Link(_linkGenerator.GetUriByAction(httpContext, "GetAllEntities", values: new { })
            , rel: "self", method: "GET");

         entitiesCollection.Links!.Add(links);

         return entitiesCollection;
      }


      private LinkResponse ReturnShapedEntity(IEnumerable<T> entities, string fields)
      {
         var shapedEntities = new DataShaper<T>().ShapeData(entities, fields).ToList();

         return new LinkResponse { ShapedEntities = shapedEntities };
      }


      private Guid GetEntityId(T t, string name)
      {
         return (Guid)t.GetType()
               .GetProperties(BindingFlags.Instance | BindingFlags.Public)
               .Where(p => p.PropertyType == typeof(Guid) && p.Name.StartsWith(name))
               .SingleOrDefault()!
               .GetValue(t)!;
      }


   }
}
