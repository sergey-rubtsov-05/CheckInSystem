using System.Linq;
using Models;
using Models.DTO;

namespace Business
{
    public interface ICheckInService
    {
        CheckIn Add(CheckInDto checkInDto);
        IQueryable<CheckIn> Get();
        void Delete(int id);
        CheckIn Update(int id, CheckInDto checkInDto);
    }
}