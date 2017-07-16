using Models;

namespace DataAccess.Impl
{
    public class PersonRepository : BaseRepository<Person>
    {
        public PersonRepository(IUnitOfWork uow) : base(uow)
        {
        }
    }
}