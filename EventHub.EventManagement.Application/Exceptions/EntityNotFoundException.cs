
using EventHub.EventManagement.Domain.Entities;
using EventHub.EventManagement.Domain.Entities.EventEntities;
using EventHub.EventManagement.Domain.Entities.OrganizationEntities;
using EventHub.EventManagement.Domain.Entities.ProducerEntities;

namespace EventHub.EventManagement.Application.Exceptions
{
   public class EntityNotFoundException<T> : NotFoundException
      where T : class
   {
      public EntityNotFoundException(string keyName, object keyValue)
         : base($"the {typeof(T).Name} with {keyName}: {keyValue} dosn't exist in the database")
      {

      }
   }

   public class CategoryNotFoundException : EntityNotFoundException<Category>
   {
      public CategoryNotFoundException(string keyName, object keyValue)
         : base(keyName, keyValue)
      {
      }
   }

   public class EventNotFoundException : EntityNotFoundException<Event>
   {
      public EventNotFoundException(string keyName, object keyValue)
         : base(keyName, keyValue)
      {
      }
   }

   public class OrganizationNotFoundException : EntityNotFoundException<Organization>
   {
      public OrganizationNotFoundException(string keyName, object keyValue) : base(keyName, keyValue)
      {
      }
   }

   public class ProducerNotFoundException : EntityNotFoundException<Producer>
   {
      public ProducerNotFoundException(string keyName, object keyValue) : base(keyName, keyValue)
      {
      }
   }

   public class MediumNotFoundException : EntityNotFoundException<Medium>
   {
      public MediumNotFoundException(string keyName, object keyValue) : base(keyName, keyValue)
      {
      }
   }

   public class FollowerNotFoundException : EntityNotFoundException<Follower>
   {
      public FollowerNotFoundException(string keyName, object keyValue) : base(keyName, keyValue)
      {
      }
   }

   public class AttendantNotFoundException : EntityNotFoundException<Attendant>
   {
      public AttendantNotFoundException(string keyName, object keyValue) : base(keyName, keyValue)
      {
      }
   }

   public class SpeakerNotFoundException : EntityNotFoundException<OrganizationEventSpeaker>
   {
      public SpeakerNotFoundException(string keyName, object keyValue) : base(keyName, keyValue)
      {
      }
   }
}
