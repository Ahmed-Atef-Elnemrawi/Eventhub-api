using EventHub.EventManagement.Application.Contracts.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventHub.EventManagement.Presistence.Repositories
{
   public class BaseRepository<T> : IBaseRepository<T> where T : class
   {
      protected readonly RepositoryContext _dbContext;

      public BaseRepository(RepositoryContext dbContext) =>
         _dbContext = dbContext;

      public void Create(T entity) =>
         _dbContext.Set<T>().Add(entity);


      public void Delete(T entity) =>
         _dbContext.Set<T>().Remove(entity);

      public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
         !trackChanges
            ? _dbContext.Set<T>().Where(expression).AsNoTracking()
            : _dbContext.Set<T>().Where(expression);


      public IQueryable<T> FindAll(bool trackChanges) =>
         !trackChanges
            ? _dbContext.Set<T>().AsNoTracking()
            : _dbContext.Set<T>();


      public void Update(T entity) =>
         _dbContext.Set<T>().Update(entity);

      public bool Exists(Expression<Func<T, bool>> expression) =>
         _dbContext.Set<T>().Any(expression);
   }
}

