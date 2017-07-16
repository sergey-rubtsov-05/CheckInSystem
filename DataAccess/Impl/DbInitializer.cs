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

            var checkIns = new[]
            {
                new CheckIn
                {
                    Person = new Person
                    {
                        FirstName = "Пётр",
                        LastName = "Петров",
                        BirthDate = new DateTime(1990, 9, 15),
                        Sex = Sex.Male
                    },
                    VisitDateTime = new DateTime(2017, 07, 15, 12, 00, 00)
                },
                new CheckIn
                {
                    Person = new Person
                    {
                        FirstName = "Анжелика",
                        LastName = "Андреева",
                        BirthDate = new DateTime(1991, 7, 16),
                        Sex = Sex.Female
                    },
                    VisitDateTime = new DateTime(2017, 07, 15, 13, 00, 00)
                },
                new CheckIn
                {
                    Person = new Person
                    {
                        FirstName = "Андрей",
                        LastName = "Жилин",
                        BirthDate = new DateTime(1992, 8, 29),
                        Sex = Sex.Male
                    },
                    VisitDateTime = new DateTime(2017, 07, 16, 19, 00, 00)
                }
            };
            context.CheckIns.AddRange(checkIns);

            context.SaveChanges();
        }
    }
}