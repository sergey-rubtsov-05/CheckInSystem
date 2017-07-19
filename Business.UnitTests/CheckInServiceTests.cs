using System;
using System.Collections.Generic;
using System.Linq;
using Business.Exceptions;
using DataAccess;
using Models;
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
        //[Fact]
        //public void AddCheckIn()
        //{
        //    var substituteRepo = Substitute.For<IRepository<CheckIn>>();
        //    var checkInService = new CheckInService(substituteRepo, Substitute.For<IUnitOfWork>());
        //    var checkIn = new CheckInDto();

        //    checkInService.Add(checkIn);

        //    Assert.NotEqual(DateTime.MinValue, checkIn.VisitDateTime);
        //    substituteRepo.Received().Add(checkIn);
        //}
    }
}
