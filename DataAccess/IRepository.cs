using System.Linq;
using Models;

namespace DataAccess
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> Get();
        T Get(int id);
        void Create(T entity);
        void Remove(T entity);
    }
}