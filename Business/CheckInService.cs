using System;
using DataAccess;
using Models;

namespace Business
{
    public class CheckInService : ICheckInService
    {
        private readonly IRepository<CheckIn> _checkInRepository;
        private readonly IUnitOfWork _uow;

        public CheckInService(IRepository<CheckIn> checkInRepository, IUnitOfWork uow)
        {
            _checkInRepository = checkInRepository;
            _uow = uow;
        }

        public void Add(CheckIn checkIn)
        {
            using (var transaction = _uow.BeginTransaction())
            {
                checkIn.VisitDateTime = DateTime.Now;
                _checkInRepository.Add(checkIn);
                transaction.Commit();
            }
        }
    }

    public interface ICheckInService
    {
        void Add(CheckIn checkIn);
    }
}
