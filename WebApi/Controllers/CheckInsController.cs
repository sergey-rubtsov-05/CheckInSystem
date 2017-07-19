using System.Collections.Generic;
using System.Linq;
using Business;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CheckInsController : Controller
    {
        private readonly ICheckInService _checkInService;

        public CheckInsController(ICheckInService checkInService)
        {
            _checkInService = checkInService;
        }

        [HttpGet]
        public IEnumerable<CheckInDto> Get()
        {
            var checkIns = _checkInService.Get();
            return checkIns
                .Select(o => MapCheckInToDto(o));
        }

        private CheckInDto MapCheckInToDto(CheckIn checkIn)
        {
            return new CheckInDto
            {
                CheckInId = checkIn.Id,
                CheckInVisitDateTime = checkIn.VisitDateTime,
                PersonBirthDate = checkIn.Person.BirthDate,
                PersonFirstName = checkIn.Person.FirstName,
                PersonLastName = checkIn.Person.LastName,
                PersonSex = checkIn.Person.Sex
            };
        }

        [HttpPost]
        public void Post(CheckInDto checkIn)
        {
            _checkInService.Add(checkIn);
        }

        [HttpPut("{id}")]
        public CheckInDto Put(int id, [FromBody] CheckInDto checkIn)
        {
            var updatedCheckIn = _checkInService.Update(id, checkIn);
            return MapCheckInToDto(updatedCheckIn);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _checkInService.Delete(id);
        }
    }
}