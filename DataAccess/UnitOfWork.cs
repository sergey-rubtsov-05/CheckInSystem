using System.Linq;
using Models;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CheckInContext _context;

        public UnitOfWork(CheckInContext context)
        {
            _context = context;
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return _context.Set<T>();
        }

        public ITransaction BeginTransaction()
        {
            throw new System.NotImplementedException();
        }

        public void Add<T>(T entity) where T : Entity
        {
            _context.Add(entity);
            _context.SaveChanges();
        }
    }
}