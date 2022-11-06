using EventHub.EventManagement.Application.Contracts.Persistance;
using EventHub.EventManagement.Application.Contracts.Persistance.EventPersistence;
using EventHub.EventManagement.Application.Contracts.Persistance.OrganizationPersistence;
using EventHub.EventManagement.Application.Contracts.Persistance.ProducerPersistence;
using EventHub.EventManagement.Persistance.Repositories.EventRepositories;
using EventHub.EventManagement.Persistance.Repositories.OrganizationRepositories;
using EventHub.EventManagement.Persistance.Repositories.ProducerRepositories;
using EventHub.EventManagement.Presistence;

namespace EventHub.EventManagement.Persistance.Repositories
{
   internal sealed class RepositoryManager : IRepositoryManager
   {
      private readonly Lazy<IAttendantRepository> _attendantRepository;
      private readonly Lazy<ICategoryRepository> _categoryRepository;
      private readonly Lazy<IEventRepository> _eventRepository;
      private readonly Lazy<IMediumRepository> _mediumRepository;

      private readonly Lazy<IOrganizationEventsRepository> _organizationEventsRepository;
      private readonly Lazy<IOrganizationEventSpeakersRepository> _organizationEventSpeakersRepository;
      private readonly Lazy<IOrganizationFollowersRepsoitory> _organizationFollowersRepsoitory;
      private readonly Lazy<IOrganizationRepository> _organizationRepository;
      private readonly Lazy<ISpeakerRepository> _organizationSpeakersRepository;

      private readonly Lazy<IProducerEventsRepository> _producerEventsRepository;
      private readonly Lazy<IProducerFollowersRepository> _prdoucerFollowersRepository;
      private readonly Lazy<IProducerRepository> _producerRepository;
      private readonly RepositoryContext _dbContext;



      public RepositoryManager(RepositoryContext dbContext)
      {
         _dbContext = dbContext;

         _attendantRepository =
            new Lazy<IAttendantRepository>(() =>
            new AttendantRepository(_dbContext));

         _producerRepository =
            new Lazy<IProducerRepository>(() =>
            new ProducersRepository(_dbContext));

         _categoryRepository =
            new Lazy<ICategoryRepository>(() =>
            new CategoriesRepository(_dbContext));

         _organizationRepository =
            new Lazy<IOrganizationRepository>(() =>
            new OrganizationRepository(_dbContext));

         _organizationFollowersRepsoitory =
            new Lazy<IOrganizationFollowersRepsoitory>(() =>
            new OrganizationFollowersRepository(_dbContext));

         _organizationEventSpeakersRepository =
               new Lazy<IOrganizationEventSpeakersRepository>(() =>
               new OrganizationEventSpeakerRepository(_dbContext));

         _producerEventsRepository =
            new Lazy<IProducerEventsRepository>(() =>
            new ProducerEventRepositroy(_dbContext));

         _prdoucerFollowersRepository =
            new Lazy<IProducerFollowersRepository>(() =>
            new ProducerFollowersRepository(_dbContext));

         _mediumRepository =
            new Lazy<IMediumRepository>(() =>
            new MediumRepository(_dbContext));

         _eventRepository =
            new Lazy<IEventRepository>(() =>
            new EventRepository(_dbContext));

         _organizationEventsRepository =
            new Lazy<IOrganizationEventsRepository>(() =>
            new OrganizationEventRepository(_dbContext));

         _organizationSpeakersRepository =
            new Lazy<ISpeakerRepository>(() =>
            new OrganizationSpeakerRepository(_dbContext));


      }


      public IAttendantRepository AttendantRepository =>
         _attendantRepository.Value;

      public IProducerRepository ProducerRepository =>
         _producerRepository.Value;

      public IOrganizationRepository OrganizationRepository =>
         _organizationRepository.Value;

      public IOrganizationEventsRepository OrganizationEventsRepository =>
         _organizationEventsRepository.Value;

      public IOrganizationFollowersRepsoitory OrganizationFollowersRepository =>
         _organizationFollowersRepsoitory.Value;

      public ICategoryRepository CategoryRepository =>
         _categoryRepository.Value;

      public IProducerEventsRepository ProducerEventsRepository =>
         _producerEventsRepository.Value;

      public IProducerFollowersRepository ProducerFollowersRepository =>
         _prdoucerFollowersRepository.Value;

      public IMediumRepository MediumRepository =>
         _mediumRepository.Value;

      public IEventRepository EventRepository =>
         _eventRepository.Value;

      public IOrganizationEventSpeakersRepository OrganizationEventSpeakersRepository =>
         _organizationEventSpeakersRepository.Value;

      public ISpeakerRepository SpeakerRepositoy =>
         _organizationSpeakersRepository.Value;


      public async Task SaveAsync() =>
         await _dbContext.SaveChangesAsync();
   }
}
