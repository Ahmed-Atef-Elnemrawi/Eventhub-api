using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;

namespace EventHub.EventManagement.Application.Contracts.Persistance.ProducerPersistence
{
   public interface IProducerEventsRepository
   {
      Task<PagedListTypeEvent<ProducerEvent>> GetAllProducerEventsAsync(Guid producerId, EventParams eventParams, bool trackChanges);
      Task<ProducerEvent?> GetProducerEventAsync(Guid producerId, Guid eventId, bool trackChanges);
      Task<PagedList<ProducerEvent>> GetAllEventsAsync(Guid attendantId, EventParams eventParams, bool trackChanges);
      Task<List<DateOnly>> GetDistictAttendantEventsDateAsync(Guid attendantId, bool trackChanges);
      Task<List<ProducerEvent>> GetCurrentDayEvents(Guid attendantId, bool trackChanges);
      Task<int> GetCurrenctDayEventsCount(Guid attendantId, bool trackChanges);
      void CreateProducerEvent(Guid producerId, ProducerEvent producerEvent);
      void UpdateProducerEvent(ProducerEvent producerEvent);
      void RemoveProducerEvent(ProducerEvent producerEvent);
   }
}
