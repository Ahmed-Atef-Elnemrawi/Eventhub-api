using EventHub.EventManagement.Domain.Common;

namespace EventHub.EventManagement.Domain.Entities.EventEntities
{
   public class Attendant : User, ISortableEntity, ISearchableEntity, IEquatable<Attendant?>
   {
      public Guid AttendantId { get; set; }
      public Guid EventId { get; set; }
      public Event? Event { get; set; }

      public override bool Equals(object? obj)
      {
         return Equals(obj as Attendant);
      }

      public bool Equals(Attendant? other)
      {
         return other is not null &&
                UserName == other.UserName &&
                Email == other.Email &&
                PhoneNumber == other.PhoneNumber &&
                FirstName == other.FirstName &&
                LastName == other.LastName &&
                Age == other.Age &&
                Genre == other.Genre &&
                LiveIn == other.LiveIn &&
                FullName == other.FullName &&
                AttendantId.Equals(other.AttendantId) &&
                EventId.Equals(other.EventId);
      }

      public static bool operator ==(Attendant? left, Attendant? right)
      {
         return EqualityComparer<Attendant>.Default.Equals(left, right);
      }

      public static bool operator !=(Attendant? left, Attendant? right)
      {
         return !(left == right);
      }

      public override int GetHashCode()
      {
         throw new NotImplementedException();
      }
   }



}