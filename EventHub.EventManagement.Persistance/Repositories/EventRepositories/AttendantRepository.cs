using EventHub.EventManagement.Application.Contracts.Persistance.EventPersistence;
using EventHub.EventManagement.Application.RequestFeatures.Paging;
using EventHub.EventManagement.Application.RequestFeatures.Params;
using EventHub.EventManagement.Domain.Entities.EventEntities;
using EventHub.EventManagement.Persistance.Repositories.RepositoriesExtensions;
using EventHub.EventManagement.Presistence;
using EventHub.EventManagement.Presistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventHub.EventManagement.Persistance.Repositories.EventRepositories
{
   public sealed class AttendantRepository : BaseRepository<Attendant>, IAttendantRepository
   {
      public AttendantRepository(RepositoryContext dbContext) : base(dbContext)
      {
      }

      public void CreateAttendant(Guid eventId, Attendant attendant)
      {

         var result = FindByCondition(a => a.AttendantId.Equals(attendant.AttendantId), false).SingleOrDefault();

         if (result is null)
            Create(attendant);

         _dbContext.EventsAttendants?.Add(new EventAttendant
         {
            EventId = eventId,
            AttendantId = attendant.AttendantId
         });



      }

      public async Task<EventAttendant?> GetEventAttendantAsync(Guid attendantId, Guid eventId)
      {
         return await _dbContext.EventsAttendants!.Where(
            a => a.AttendantId.Equals(attendantId)
            && a.EventId.Equals(eventId)).SingleOrDefaultAsync();
      }

      public async Task<Attendant?> GetAttendantAsync
         (Guid eventId, Guid AttendantId, bool trackChanges)
      {
         return
            await FindByCondition(a =>
            a.Events!.Any(e => e.EventId.Equals(eventId)) &&
            a.AttendantId.Equals(AttendantId),
            trackChanges)
            .Include(a => a.Events)
            .SingleOrDefaultAsync();
      }

      public async Task<PagedList<Attendant>> GetAllAttendantsAsync
         (Guid eventId, AttendantParams attendantParams, bool trackChanges)
      {
         var attendants =
               await FindByCondition(a =>
               a.Events!.Any(e => e.EventId.Equals(eventId)),
               trackChanges).Search(attendantParams.SearchTerm)
               .Sort(attendantParams.SortBy)
               .Skip((attendantParams.PageNumber - 1) * attendantParams.PageSize)
               .Take(attendantParams.PageSize)
               .ToListAsync();

         var count = await FindByCondition(a =>
         a.Events!.Any(e => e.EventId.Equals(eventId)), trackChanges).CountAsync();


         return new PagedList<Attendant>
            (attendants, count, attendantParams.PageNumber, attendantParams.PageSize);

      }

      public void RemoveAttendant(Attendant attendant) =>
         Delete(attendant);

      public void RemoveEventAttendant(EventAttendant eventAttendant)
      {
         _dbContext.EventsAttendants!.Remove(eventAttendant);
      }
   }
}
