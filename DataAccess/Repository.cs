using System.Linq;
using Models;

namespace DataAccess
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly IUnitOfWork _uow;

        public Repository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IQueryable<T> Get()
        {
            return _uow.Query<T>();
        }

        public T Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(T entity)
        {
            _uow.Add(entity);
        }

        public void Remove(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}