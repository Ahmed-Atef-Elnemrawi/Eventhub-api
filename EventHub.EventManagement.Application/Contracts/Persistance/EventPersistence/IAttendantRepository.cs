using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.EventEntities;

namespace EventHub.EventManagement.Application.Contracts.Persistance.EventPersistence
{
   public interface IAttendantRepository
   {
      Task<PagedList<Attendant>> GetAllAttendantsAsync(Guid eventId, AttendantParams attendantParams, bool trackChanges);
      Task<Attendant?> GetAttendantAsync(Guid eventId, Guid AttendantId, bool trackChanges);
      Task<EventAttendant?> GetEventAttendantAsync(Guid attendantId, Guid eventId);
      void CreateAttendant(Guid eventId, Attendant attendant);
      void RemoveAttendant(Attendant attendant);
      void RemoveEventAttendant(EventAttendant eventAttendant);
   }
}
