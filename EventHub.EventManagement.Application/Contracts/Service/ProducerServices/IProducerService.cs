using EventHub.EventManagement.Application.DTOs.ProducerDto;
using EventHub.EventManagement.Application.Models.LinkModels;
using EventHub.EventManagement.Application.RequestFeatures.Paging;

namespace EventHub.EventManagement.Application.Contracts.Service.ProducerServices
{
   public interface IProducerService
   {
      Task<(LinkResponse link, MetaData metaData)> GetAllProducersAsync
         (ProducerLinkParams producerLinkParams, bool trackChanges);

      Task<LinkResponse> GetProducerAsync(Guid id, ProducerLinkParams linkParams, bool trackChanges);

      Task<ProducerDto> CreateProducerAsync(ProducerForCreationDto producer);

      Task UpdateProducerAsync(
         Guid producerId, ProducerForUpdateDto producerForUpdate, bool trackChanges);

      Task RemoveProducerAsync(Guid producerId, bool trackChanges);

      Task<LinkResponse> GetAllProducersAsync(Guid followerId,
                                               ProducerLinkParams linkParams,
                                               bool trackChanges);

      Task<bool> CheckIfProducerExist(Guid followerId, Guid producerId, bool trackChanges);

   }
}
