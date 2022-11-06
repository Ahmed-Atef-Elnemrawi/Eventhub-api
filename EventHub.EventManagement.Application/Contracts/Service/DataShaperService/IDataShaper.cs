using EventHub.EventManagement.Application.Models;

namespace EventHub.EventManagement.Application.Contracts.Service.DataShapingService
{
   public interface IDataShaper<T>
   {
      IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string? fieldsString);
      ShapedEntity ShapeData(T entity, string? fieldsString);
   }
}
