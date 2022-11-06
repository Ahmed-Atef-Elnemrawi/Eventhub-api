using EventHub.EventManagement.Application.Contracts.Persistance.EventPersistence;
using EventHub.EventManagement.Application.Contracts.Persistance.OrganizationPersistence;
using EventHub.EventManagement.Application.Contracts.Persistance.ProducerPersistence;

namespace EventHub.EventManagement.Application.Contracts.Persistance
{
   public interface IRepositoryManager
   {
      IAttendantRepository AttendantRepository { get; }
      ICategoryRepository CategoryRepository { get; }
      IEventRepository EventRepository { get; }
      IMediumRepository MediumRepository { get; }

      IOrganizationEventsRepository OrganizationEventsRepository { get; }
      IOrganizationEventSpeakersRepository OrganizationEventSpeakersRepository { get; }
      IOrganizationFollowersRepsoitory OrganizationFollowersRepository { get; }
      IOrganizationRepository OrganizationRepository { get; }
      ISpeakerRepository SpeakerRepositoy { get; }

      IProducerEventsRepository ProducerEventsRepository { get; }
      IProducerFollowersRepository ProducerFollowersRepository { get; }
      IProducerRepository ProducerRepository { get; }

      Task SaveAsync();
   }
}
