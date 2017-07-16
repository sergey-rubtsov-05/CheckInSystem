using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Impl
{
    public class CheckInContext : DbContext
    {
        public CheckInContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
