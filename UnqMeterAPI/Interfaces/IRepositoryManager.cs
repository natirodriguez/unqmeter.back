using System.Linq.Expressions;

namespace UnqMeterAPI.Interfaces
{
    public interface IRepositoryManager<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Save();
        void Add(T t);
        void Edit(T entity);
        void Delete(T entity);
    }
}
