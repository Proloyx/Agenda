using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Agenda.Models;
using Agenda.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Agenda.Services;
using Agenda.Models.CalendarModels;
using System.Text.Json;
using System.Security.Claims;

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
        var calendar =new Calendar{ 
            Date = DateOnly.FromDateTime(DateTime.Now), 
            User = JsonSerializer.Deserialize<User>(User.FindFirstValue("user"))
            }; 
        return View(calendar);
    }

    public IActionResult UpdateCalendar(DateSubmit dateSubmit) {
        var calendar = new Calendar{ 
            Date = new DateOnly(dateSubmit.Years, dateSubmit.Months, 1),
            User = JsonSerializer.Deserialize<User>(User.FindFirstValue("user"))
            };
        return View("index", calendar);
        //return PartialView("_CalendarPartial", date);
    }
}
