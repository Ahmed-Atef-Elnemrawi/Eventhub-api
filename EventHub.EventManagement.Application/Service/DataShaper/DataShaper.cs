using EventHub.EventManagement.Application.Contracts.Service.DataShapingService;
using EventHub.EventManagement.Application.Models;
using System.Reflection;

namespace EventHub.EventManagement.Application.Service.DataShaper
{
   public class DataShaper<T> : IDataShaper<T>
      where T : class
   {
      public PropertyInfo[] Properties { get; set; }

      public DataShaper()
      {
         Properties = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance);
      }

      public IEnumerable<ShapedEntity> ShapeData
         (IEnumerable<T> entities, string? fieldsString)
      {
         var requiredProperties = GetRequiredProperties(fieldsString, Properties);
         return FetchData(entities, requiredProperties);
      }


      public ShapedEntity ShapeData(T entity, string? fieldsString)
      {
         var requiredProperties = GetRequiredProperties(fieldsString, Properties);
         return FetchDataForEntity(entity, requiredProperties);
      }


      private IEnumerable<PropertyInfo> GetRequiredProperties
         (string? fieldsString, IEnumerable<PropertyInfo> properties)
      {
         var requiredProperties = new List<PropertyInfo>();

         if (!string.IsNullOrWhiteSpace(fieldsString))
         {
            var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var field in fields)
            {
               var property =
                  properties.FirstOrDefault(p =>
                  p.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));

               if (property is null)
                  continue;

               requiredProperties.Add(property);
            }

         }

         else
            requiredProperties = properties.ToList();

         return requiredProperties;
      }


      private ShapedEntity FetchDataForEntity
     (T entity, IEnumerable<PropertyInfo> requiredProperties)
      {
         var shapedObject = new ShapedEntity();

         foreach (var property in requiredProperties)
         {
            var entityPropertyValue = property.GetValue(entity);
            shapedObject.Entity!.TryAdd(property.Name, entityPropertyValue);
         }

         var id = entity.GetType().GetProperties().FirstOrDefault(p => p.Name.Contains("Id"))?.GetValue(entity);
         shapedObject.Id = (Guid)id!;

         return shapedObject;
      }


      private IEnumerable<ShapedEntity> FetchData
        (IEnumerable<T> entities, IEnumerable<PropertyInfo> properties)
      {
         var shapedObjects = new List<ShapedEntity>();

         foreach (var entity in entities)
         {
            var shapedObject = FetchDataForEntity(entity, properties);
            shapedObjects.Add(shapedObject);
         }

         return shapedObjects;
      }

   }
}
