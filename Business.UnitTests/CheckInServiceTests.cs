using System;
using System.Collections.Generic;
using System.Linq;
using Business.Exceptions;
using DataAccess;
using Models;
using Models.DTO;
using NSubstitute;
using Xunit;

namespace Business.UnitTests
{
    public class CheckInServiceTests
    {
        private readonly List<CheckIn> _checkIns;
        private readonly CheckInService _checkInService;
        private readonly IUnitOfWork _stubUow;

        public CheckInServiceTests()
        {
            var earlierDate = new DateTime(2017, 07, 16, 17, 00, 00);
            var laterDate = new DateTime(2017, 07, 16, 18, 00, 00);
            _checkIns = new List<CheckIn>
            {
                new CheckIn { Person = new Person(), VisitDateTime = earlierDate },
                new CheckIn { Person = new Person(), VisitDateTime = laterDate }
            };

            _stubUow = Substitute.For<IUnitOfWork>();
            _stubUow.Query<CheckIn>().Returns(info => _checkIns.AsQueryable());
            _checkInService = new CheckInService(_stubUow);
        }

        [Fact]
        public void GetReturnOrderedByVisitDateTime()
        {
            var checkIns = _checkInService.Get().ToList();

            Assert.True(checkIns.First().VisitDateTime > checkIns.Last().VisitDateTime);
        }

        [Fact]
        public void GetReturnWithoutDeleted()
        {
            _checkIns.Add(new CheckIn { Person = new Person(), IsDeleted = true });

            var checkIns = _checkInService.Get();

            Assert.False(checkIns.Any(o => o.IsDeleted));
        }

        [Fact]
        public void DeleteExistingNotDeletedCheckIn()
        {
            var checkIn = new CheckIn { Id = 1, Person = new Person(), IsDeleted = false };
            _checkIns.Add(checkIn);

            _checkInService.Delete(1);

            Assert.True(checkIn.IsDeleted);
            _stubUow.Received().SaveChanges();
        }

        [Fact]
        public void DeleteExistingDeletedCheckIn()
        {
            var checkIn = new CheckIn { Id = 2, Person = new Person(), IsDeleted = true };
            _checkIns.Add(checkIn);

            _checkInService.Delete(2);

            Assert.True(checkIn.IsDeleted);
            _stubUow.DidNotReceive().SaveChanges();
        }

        [Fact]
        public void DeleteNotExistingThrowNotFoundException()
        {
            Assert.Throws<NotFoundExpection>(() => _checkInService.Delete(3));
        }

        [Fact]
        public void AddCheckInWithExistingPerson()
        {
            _stubUow.Query<Person>().Returns(info => new List<Person>
            {
                new Person
                {
                    FirstName = "firstName",
                    LastName = "lastName",
                    BirthDate = new DateTime(2017, 7, 19),
                    Sex = Sex.Female
                }
            }.AsQueryable());
            var checkInDto = new CheckInDto
            {
                PersonFirstName = "firstName",
                PersonLastName = "lastName",
                PersonBirthDate = new DateTime(2017, 7, 19),
                PersonSex = Sex.Female
            };

            var checkIn = _checkInService.Add(checkInDto);

            Assert.NotEqual(DateTime.MinValue, checkIn.VisitDateTime);
            _stubUow.Received().Add(checkIn);
        }

        [Fact]
        public void AddCheckInWithNewPerson()
        {
            var checkInDto = new CheckInDto();

            var checkIn = _checkInService.Add(checkInDto);

            Assert.NotEqual(DateTime.MinValue, checkIn.VisitDateTime);
            _stubUow.Received().Add(checkIn);
        }

        [Fact]
        public void AddCheckInWithTransaction()
        {
            var checkInDto = new CheckInDto();
            var stubTransaction = Substitute.For<ITransaction>();
            _stubUow.BeginTransaction().Returns(info => stubTransaction);

            _checkInService.Add(checkInDto);

            _stubUow.Received().BeginTransaction();
            stubTransaction.Received().Commit();
        }

        [Fact]
        public void UpdateNotExistingCheckInThrowsNotFoundException()
        {
            Assert.Throws<NotFoundExpection>(() => _checkInService.Update(10, null));
        }

        [Fact]
        public void UpdateCheckIn()
        {
            _checkIns.Add(new CheckIn
            {
                Id = 10,
                Person = new Person()
            });
            var checkInDto = new CheckInDto
            {
                PersonFirstName = "newFirstName",
                PersonLastName = "newLastName",
                PersonSex = Sex.Female,
                PersonBirthDate = new DateTime(2000, 1, 1)
            };

            var checkIn = _checkInService.Update(10, checkInDto);

            Assert.Equal("newFirstName", checkIn.Person.FirstName);
            Assert.Equal("newLastName", checkIn.Person.LastName);
            Assert.Equal(Sex.Female, checkIn.Person.Sex);
            Assert.Equal(new DateTime(2000, 1, 1), checkIn.Person.BirthDate);

            _stubUow.Received().SaveChanges();
        }
    }
}
