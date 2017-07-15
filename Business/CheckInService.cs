using System;
using DataAccess;
using Models;

namespace Business
{
    public class CheckInService : ICheckInService
    {
        private readonly IRepository<CheckIn> _checkInRepository;

        public CheckInService(IRepository<CheckIn> checkInRepository)
        {
            _checkInRepository = checkInRepository;
        }

        public void Add(CheckIn checkIn)
        {
            checkIn.VisitDateTime = DateTime.Now;
            _checkInRepository.Add(checkIn);
        }
    }

    public interface ICheckInService
    {
        void Add(CheckIn checkIn);
    }
}
