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
                new CheckIn
                {
                    Person = new Person
                    {
                        FirstName = "Пётр",
                        LastName = "Петров",
                        BirthDate = new DateTime(1990, 9, 10),
                        Sex = Sex.Male
                    },
                    VisitDateTime = DateTime.Now
                },
                new CheckIn
                {
                    Person = new Person
                    {
                        FirstName = "Анжелика",
                        LastName = "Андреева",
                        BirthDate = new DateTime(1991, 7, 3),
                        Sex = Sex.Female
                    },
                    VisitDateTime = DateTime.Now
                }
            };
            context.CheckIns.AddRange(checkIns);

            context.SaveChanges();
        }
    }
}