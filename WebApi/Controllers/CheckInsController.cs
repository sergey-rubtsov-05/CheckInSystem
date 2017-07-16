using System;
using System.Collections.Generic;
using System.Linq;
using Business;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CheckInsController : Controller
    {
        private readonly IRepository<CheckIn> _checkInRepository;
        private readonly ICheckInService _checkInService;

        public CheckInsController(IRepository<CheckIn> checkInRepository, ICheckInService checkInService)
        {
            _checkInRepository = checkInRepository;
            _checkInService = checkInService;
        }

        [HttpGet]
        public IEnumerable<CheckInDto> Get()
        {
            var checkIns = _checkInRepository.Get().Select(o => new CheckInDto
            {
                CheckInId = o.Id,
                CheckInVisitDateTime = o.VisitDateTime,
                PersonBirthDate = o.Person.BirthDate,
                PersonFullName = $"{o.Person.FirstName} {o.Person.LastName}",
                PersonSex = o.Person.Sex
            });
            return checkIns;
        }

        [HttpPost]
        public void Post(CheckIn checkIn)
        {
            _checkInService.Add(checkIn);
        }
    }
}