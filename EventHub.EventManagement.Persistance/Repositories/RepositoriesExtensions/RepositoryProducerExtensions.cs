using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;

namespace EventHub.EventManagement.Persistance.Repositories.RepositoriesExtensions
{
   internal static class RepositoryProducerExtensions
   {
      public static IQueryable<Producer> Filter
        (this IQueryable<Producer> collection, ProducerParams producerParams)
      {
         if (!string.IsNullOrEmpty(producerParams.JobTitle))
            return collection.Where(p => p.JobTitle == producerParams.JobTitle);

         return collection;
      }

   }
}
