using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Business;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CheckInsController : Controller
    {
        private readonly ICheckInService _checkInService;
        private readonly IMapper _mapper;

        public CheckInsController(ICheckInService checkInService, IMapper mapper)
        {
            _checkInService = checkInService;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<CheckInDto> Get()
        {
            var checkIns = _checkInService.Get();
            return checkIns.Select(_mapper.Map<CheckInDto>);
        }

        [HttpPost]
        public void Post(CheckInDto checkIn)
        {
            _checkInService.Add(checkIn);
        }

        [HttpPut("{id}")]
        public CheckInDto Put(int id, [FromBody] CheckInDto checkInDto)
        {
            var updatedCheckIn = _checkInService.Update(id, checkInDto);
            return _mapper.Map<CheckInDto>(updatedCheckIn);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _checkInService.Delete(id);
        }
    }
}