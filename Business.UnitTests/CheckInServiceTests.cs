using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Models;
using Models.DTO;
using NSubstitute;
using Xunit;

namespace Business.UnitTests
{
    public class CheckInServiceTests
    {
        [Fact]
        public void GetReturnOrderedByVisitDateTime()
        {
            var earlierDate = new DateTime(2017, 07, 16, 17, 00, 00);
            var laterDate = new DateTime(2017, 07, 16, 18, 00, 00);
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.Query<CheckIn>().Returns(info => new List<CheckIn>
            {
                new CheckIn { Person = new Person(), VisitDateTime = earlierDate },
                new CheckIn { Person = new Person(), VisitDateTime = laterDate }
            }.AsQueryable());
            var checkInService = new CheckInService(Substitute.For<IRepository<CheckIn>>(), unitOfWork);

            var checkIns = checkInService.Get().ToList();

            Assert.Equal(laterDate, checkIns.First().CheckInVisitDateTime);
            Assert.Equal(earlierDate, checkIns.Last().CheckInVisitDateTime);
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
