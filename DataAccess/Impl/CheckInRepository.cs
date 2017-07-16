using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Impl
{
    public class CheckInRepository : BaseRepository<CheckIn>
    {
        public CheckInRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public override IQueryable<CheckIn> Get()
        {
            return EntityFrameworkQueryableExtensions.Include<CheckIn, Person>(base.Get(), o => o.Person);
        }
    }
}