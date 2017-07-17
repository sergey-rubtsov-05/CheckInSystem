using System.Linq;
using Models;

namespace DataAccess.Impl
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
            if (_context.Database.CurrentTransaction == null)
                return new DbTransaction(_context.Database.BeginTransaction());

            return new MockTransaction();
        }

        public void Add<T>(T entity) where T : Entity
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Remove<T>(T entity) where T : Entity
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}