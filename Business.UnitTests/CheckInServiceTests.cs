using System;
using DataAccess;
using Models;
using NSubstitute;
using Xunit;

namespace Business.UnitTests
{
    public class CheckInServiceTests
    {
        [Fact]
        public void AddCheckIn()
        {
            var substituteRepo = Substitute.For<IRepository<CheckIn>>();
            var checkInService = new CheckInService(substituteRepo);
            var checkIn = new CheckIn();

            checkInService.Add(checkIn);

            Assert.NotEqual(DateTime.MinValue, checkIn.VisitDateTime);
            substituteRepo.Received().Add(checkIn);
        }
    }
}
