using System;
using System.Collections.Generic;
using System.Linq;
using Business;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Models;

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
        public IEnumerable<CheckIn> Get()
        {
            return _checkInRepository.Get().ToList();
        }

        [HttpPost]
        public void Post(CheckIn checkIn)
        {
            _checkInService.Add(checkIn);
        }
    }
}