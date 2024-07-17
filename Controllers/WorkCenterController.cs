using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Data;
using Agenda.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Agenda.Controllers
{
    public class WorkCenterController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICookieService _cookieService;

        public WorkCenterController(IMapper mapper, ICookieService cookieService)
        {
            _mapper = mapper;
            _cookieService = cookieService;
        }

        public async Task<IActionResult> Index()
        {
            var workcenters = _cookieService.GetUser().Workcenters;
              return workcenters != null ? 
                          View(workcenters.ToList()) :
                          Problem("No existen centros de trabajo");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}