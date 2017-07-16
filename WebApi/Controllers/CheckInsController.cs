using System.Collections.Generic;
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
            return checkIns;
        }

        [HttpPost]
        public void Post(CheckInDto checkIn)
        {
            _checkInService.Add(checkIn);
        }
    }
}