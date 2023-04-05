
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

   public sealed class CategoryNotFound : EntityNotFoundException<Category>
   {
      public CategoryNotFound(string keyName, object keyValue)
         : base(keyName, keyValue)
      {
      }
   }

   public sealed class EventNotFound : EntityNotFoundException<Event>
   {
      public EventNotFound(string keyName, object keyValue)
         : base(keyName, keyValue)
      {
      }
   }

   public sealed class OrganizationNotFound : EntityNotFoundException<Organization>
   {
      public OrganizationNotFound(string keyName, object keyValue) : base(keyName, keyValue)
      {
      }
   }

   public sealed class ProducerNotFound : EntityNotFoundException<Producer>
   {
      public ProducerNotFound(string keyName, object keyValue) : base(keyName, keyValue)
      {
      }
   }

   public sealed class MediumNotFound : EntityNotFoundException<Medium>
   {
      public MediumNotFound(string keyName, object keyValue) : base(keyName, keyValue)
      {
      }
   }

   public sealed class FollowerNotFound : EntityNotFoundException<Follower>
   {
      public FollowerNotFound(string keyName, object keyValue) : base(keyName, keyValue)
      {
      }
   }

   public sealed class AttendantNotFound : EntityNotFoundException<Attendant>
   {
      public AttendantNotFound(string keyName, object keyValue) : base(keyName, keyValue)
      {
      }
   }

   public sealed class SpeakerNotFound : EntityNotFoundException<OrganizationEventSpeaker>
   {
      public SpeakerNotFound(string keyName, object keyValue) : base(keyName, keyValue)
      {
      }
   }


   public sealed class UserNotFound : EntityNotFoundException<User>
   {
      public UserNotFound(string keyName, object keyValue) : base(keyName, keyValue)
      {
      }
   }


   public sealed class UserPageNotFound : EntityNotFoundException<UserPage>
   {
      public UserPageNotFound(string keyName, object keyValue) : base(keyName, keyValue)
      {

      }
   }
}
