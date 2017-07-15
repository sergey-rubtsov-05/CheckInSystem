using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CheckInsController : Controller
    {
        private readonly IRepository<CheckIn> _checkInRepository;

        public CheckInsController(IRepository<CheckIn> checkInRepository)
        {
            _checkInRepository = checkInRepository;
        }

        [HttpGet]
        public IEnumerable<CheckIn> Get()
        {
            return _checkInRepository.Get().ToList();
        }
    }
}