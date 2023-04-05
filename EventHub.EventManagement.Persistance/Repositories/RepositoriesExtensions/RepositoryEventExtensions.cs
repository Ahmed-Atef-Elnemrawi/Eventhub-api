using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.EventEntities;

namespace EventHub.EventManagement.Persistance.Repositories.RepositoriesExtensions
{
   internal static class RepositoryEventExtensions
   {
      public static IQueryable<Event> Filter(this IQueryable<Event> collection, EventParams eventParams)
      {

         if (eventParams.MediumType != MediumType.None)
            collection = collection.Where(e => e.Category!.Medium!.Type == eventParams.MediumType);

         if (!string.IsNullOrEmpty(eventParams.Category))
            collection = collection.Where(e => e.Category!.Name == eventParams.Category);

         return collection;
      }



      public static IQueryable<T> FilterByDate<T>
         (this IQueryable<T> collection, EventParams eventParams) where T : Event
      {
         if (eventParams.UpComing != false)
            return collection.Where(e => e.Date >= DateTime.Now).OrderBy(e => e.Date);

         if (eventParams.Last24Hours != false)
            return collection.Where(e => e.CreatedDate >= DateTime.Now.AddHours(-24)).OrderBy(e => e.Date);

         if (eventParams.LastWeek != false)
            return collection.Where(e => e.CreatedDate > DateTime.Now.AddDays(-7)).OrderBy(e => e.Date);

         if (eventParams.LastMonth != false)
            return collection.Where(e => e.CreatedDate >= DateTime.Now.AddMonths(-1)).OrderBy(e => e.Date);

         if (eventParams.Year > DateTime.MinValue.Year &&
            eventParams.Year < DateTime.MaxValue.Year &&
            eventParams.Year != DateTime.Now.Year)
            return collection.Where(e => e.CreatedDate.Year == eventParams.Year);

         return collection;
      }

   }
}
