using System.Linq;
using Models;

namespace DataAccess
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> Get();
        T Get(int id);
        void Add(T entity);
        void Remove(T entity);
    }
}