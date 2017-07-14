using System;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class CheckInContext : DbContext
    {
        public CheckInContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CheckIn> CheckIns { get; set; }
    }
}
