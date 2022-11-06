using EventHub.EventManagement.Application.DTOs.CategoryDto;
using EventHub.EventManagement.Application.Models;
using EventHub.EventManagement.Application.Models.LinkModels;

namespace EventHub.EventManagement.API.Utility.EventUtility
{
   public class CategoryLinks : EntityLinkes<CategoryDto>
   {
      public CategoryLinks(LinkGenerator linkGenerator) : base(linkGenerator)
      {
      }

      internal override List<Link> GenerateEntityLinks(
         HttpContext httpContext,
         Guid id,
         Guid? parentId,
         Guid? parentParentId,
         string fields = "")
      {
         var categoryId = id;
         var mediumId = parentId;

         var links = new List<Link>
         {
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetCategory",
            values: new { mediumId , id}),
            rel:"self", method:"GET"),

            new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateCategory",
            values: new {mediumId, id}),
            rel:"update-category", method:"PUT"),

            new Link(_linkGenerator.GetUriByAction(httpContext, "RemoveCategory",
            values: new {mediumId, id}),
            rel:"delete-category", method:"DELETE"),

            new Link(_linkGenerator.GetUriByAction(httpContext,"GetCategoryEvents",
            values: new{mediumId, categoryId}),
            rel:"get-category-events", method:"GET")

         };
         return links;
      }

      internal override LinkCollectionWrapper<Entity> GenerateEntitiesLinks
         (HttpContext httpContext, LinkCollectionWrapper<Entity> entitiesCollection)
      {

         var link = new Link(_linkGenerator.GetUriByAction(httpContext, "GetAllCategories", values: new { }),
            rel: "self", method: "GET");

         entitiesCollection.Links!.Add(link);

         return entitiesCollection;
      }

   }
}
