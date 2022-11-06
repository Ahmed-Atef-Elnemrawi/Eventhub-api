using EventHub.EventManagement.Domain.Common;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace EventHub.EventManagement.Persistance.Repositories.RepositoriesExtensions
{
   public static class RepositoryEntityExtension
   {
      public static IQueryable<T> Sort<T>
        (this IQueryable<T> collection, string? orderByQueryString) where T : ISortableEntity
      {
         if (string.IsNullOrWhiteSpace(orderByQueryString))
            return collection;

         var orderParams = orderByQueryString.Trim().Split(',');

         var propertyInfos = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance);

         var orderQueryBuilder = new StringBuilder();

         foreach (var param in orderParams)
         {
            if (string.IsNullOrWhiteSpace(param))
               continue;

            var propertyFromQueryName = param.Split(" ")[0];
            var objectProperty = propertyInfos.FirstOrDefault(pi =>
           pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty == null)
               continue;

            var direction = param.EndsWith(" desc") ? "descending" : "ascending";
            orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
         }
         var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
         if (string.IsNullOrWhiteSpace(orderQuery))
            return collection;


         return collection.OrderBy(orderQuery);
      }


      public static IQueryable<T> Search<T>
         (this IQueryable<T> collection, string? searchQueryString) where T : ISearchableEntity
      {
         if (string.IsNullOrWhiteSpace(searchQueryString))
            return collection;

         var propertiesInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

         var properties = propertiesInfo
            .Where(pi => pi.Name.ToLower().Contains("name") && pi.CanWrite).ToList();

         var searchQueryBuilder = new StringBuilder();

         foreach (var property in properties)
         {
            if (string.IsNullOrEmpty(property.Name))
               continue;

            searchQueryBuilder.Append($"{property.Name}.Contains(\"{searchQueryString}\") || ");

         }

         var searchQuery = searchQueryBuilder.ToString().TrimEnd('|', '|', ' ');

         if (searchQuery != null)
            return collection.Where(searchQuery);

         return collection;
      }

   }
}
