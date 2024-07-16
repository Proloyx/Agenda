using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Agenda.Models;
using Agenda.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Agenda.Services;
using Agenda.Models.CalendarModels;

namespace Agenda.Controllers;

[Authorize]
[TypeFilter(typeof(ValidateUserFilter))]
public class CalendarController : Controller
{
    //private readonly ILogger<CalendarController> _logger;
    private readonly AppDbContext _context;

    public CalendarController(/*ILogger<CalendarController> logger,*/ AppDbContext context)
    {
        //_logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View(new DateModel());
    }

    public IActionResult UpdateCalendar(DateSubmit dateSubmit) {
        var date = new DateModel{ Date = new DateOnly(dateSubmit.Years, dateSubmit.Months, 1)};
        return View("index", date);
        //return PartialView("_CalendarPartial", date);
    }
}
