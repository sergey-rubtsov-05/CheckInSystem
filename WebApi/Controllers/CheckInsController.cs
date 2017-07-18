using System;
using System.Collections.Generic;
using System.Linq;
using Business;
using Microsoft.AspNetCore.Mvc;
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
                .Select(o => new CheckInDto
                {
                    CheckInId = o.Id,
                    CheckInVisitDateTime = o.VisitDateTime,
                    PersonBirthDate = o.Person.BirthDate,
                    PersonFirstName = o.Person.FirstName,
                    PersonLastName = o.Person.LastName,
                    PersonSex = o.Person.Sex
                });
        }

        [HttpPost]
        public void Post(CheckInDto checkIn)
        {
            throw new Exception("aaa");
            _checkInService.Add(checkIn);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CheckInDto checkIn)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _checkInService.Delete(id);
        }
    }
}