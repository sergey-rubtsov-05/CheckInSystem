using System;
using System.Linq;
using Models;

namespace DataAccess.Impl
{
    public static class DbInitializer
    {
        public static void Initialize(CheckInContext context)
        {
            context.Database.EnsureCreated();

            if (context.CheckIns.Any())
            {
                return;
            }

            var checkIns = new CheckIn[]
            {
                new CheckIn { Visitor = "Петров Пётр", VisitDateTime = DateTime.Now, Sex = Sex.Male },
                new CheckIn { Visitor = "Андреева Анжелика", VisitDateTime = DateTime.Now, Sex = Sex.Female }
            };
            context.CheckIns.AddRange(checkIns);

            context.SaveChanges();
        }
    }
}