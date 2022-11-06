using System.Linq.Expressions;

namespace EventHub.EventManagement.Application.Contracts.Persistance
{
   public interface IBaseRepository<T>
   {
      void Create(T entity);
      void Delete(T entity);
      void Update(T entity);

      IQueryable<T> FindAll(bool trackChanges);
      IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);

   }
}
