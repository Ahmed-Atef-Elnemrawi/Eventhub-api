using EventHub.EventManagement.Domain.Entities.EventEntities;

namespace EventHub.EventManagement.Application.RequestFeatures.Paging
{
   public class PagedListTypeEvent<T> : List<T>
      where T : Event
   {
      public MetaDataTypeEvent MetaData { get; set; }
      public PagedListTypeEvent(List<T> items, int count, int upComingCount, int pageNumber, int pageSize)
      {
         MetaData = new MetaDataTypeEvent
         {
            TotalCount = count,
            UpcomingCount = upComingCount,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
         };

         AddRange(items);
      }
   }
}
