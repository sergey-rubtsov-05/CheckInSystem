using System.Linq;
using Models;

namespace DataAccess
{
    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>() where T : Entity;
        ITransaction BeginTransaction();
    }
}