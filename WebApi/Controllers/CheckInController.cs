using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CheckInController : Controller
    {
        private readonly CheckInContext _context;

        public CheckInController(CheckInContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<CheckIn> Get()
        {
            return _context.CheckIns.ToList();
        }
    }
}