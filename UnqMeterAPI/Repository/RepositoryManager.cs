using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UnqMeterAPI.Interfaces;

namespace UnqMeterAPI.Repository
{
    public class RepositoryManager<T> : IRepositoryManager<T> where T : class
    {
        private RepositoryContext _repositoryContext;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _repositoryContext.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _repositoryContext.Set<T>();
        }

        public void Add(T t)
        {
            _repositoryContext.Add(t);
        }

        public void Edit(T entity)
        {
            _repositoryContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _repositoryContext.Set<T>().Remove(entity);
        }
        public void Save() => _repositoryContext.SaveChanges();
    }
}
