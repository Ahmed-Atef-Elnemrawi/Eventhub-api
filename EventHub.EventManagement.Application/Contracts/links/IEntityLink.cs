using EventHub.EventManagement.Application.Models.LinkModels;
using Microsoft.AspNetCore.Http;

namespace EventHub.EventManagement.Application.Contracts.Links
{
   public interface IEntityLinks<T>
   {
      LinkResponse TryGetEntitiesLinks
         (IEnumerable<T> entitiesDto, string fields, HttpContext httpContext, Guid? parentId = null, Guid? parentParentId = null);

      LinkResponse TryGetEntityLinks
         (T entityDto, string fields, HttpContext httpContext, Guid? parentId = null, Guid? parentParentId = null);
   }
}
