using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;

namespace EventHub.EventManagement.Application.Contracts.Persistance.ProducerPersistence
{
   public interface IProducerRepository
   {
      Task<PagedList<Producer>> GetAllProducersAsync
         (ProducerParams producerParams, bool trackChanges);

      Task<Producer?> GetProducerAsync(Guid id, bool trackChanges);
      void CreateProducer(Producer producer);
      void RemoveProducer(Producer producer);
      void UpdateProducer(Producer producer);

   }
}
