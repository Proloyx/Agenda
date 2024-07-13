using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Agenda.Controllers
{
    [Authorize]
    public class HelloWorldController : Controller
    {
        private readonly ILogger<HelloWorldController> _logger;
        private readonly AppDbContext _context;

        public HelloWorldController(ILogger<HelloWorldController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }

        public IActionResult Details (int? id){
            if (id == null)
            {
                return NotFound();
            }
            
            var user = _context.Users.FirstOrDefault( u => u.Userid == id);
            if (user == null) { return NotFound();}
            return View(user);
        }
    }
    
}