using System.Linq;
using Models;

namespace DataAccess
{
    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>() where T : Entity;
        ITransaction BeginTransaction();
        void Add<T>(T entity) where T : Entity;
        void Remove<T>(T entity) where T : Entity;
        void SaveChanges();
    }
}