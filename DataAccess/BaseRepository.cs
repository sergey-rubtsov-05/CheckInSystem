using System.Linq;
using Models;

namespace DataAccess
{
    public abstract class BaseRepository<T> : IRepository<T> where T : Entity
    {
        private readonly IUnitOfWork _uow;

        protected BaseRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public virtual IQueryable<T> Get()
        {
            return _uow.Query<T>();
        }

        public virtual T Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Add(T entity)
        {
            _uow.Add(entity);
        }

        public virtual void Remove(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}