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
using Agenda.Interfaces;

namespace Agenda.Controllers;

[Authorize]
[TypeFilter(typeof(ValidateUserFilter))]
public class CalendarController : Controller
{
    private readonly ICookieService _cookieService;
    private readonly AppDbContext _context;
    public CalendarController(ICookieService cookieService, AppDbContext context)
    {
        _cookieService = cookieService;
        _context = context;
    }

    public IActionResult Index()
    {
        var user = _context.Users.FirstOrDefault(s => s.Userid == _cookieService.GetUser().Userid);
        var calendar =new Calendar{ 
            Date = DateOnly.FromDateTime(DateTime.Now), 
            User = user
            }; 
        return View(calendar);
    }

    public IActionResult UpdateCalendar(DateSubmit dateSubmit) {
        var user = _context.Users.FirstOrDefault(s => s.Userid == _cookieService.GetUser().Userid);
        var calendar = new Calendar{ 
            Date = new DateOnly(dateSubmit.Years, dateSubmit.Months, 1),
            User = user
            };
        return View("index", calendar);
        //return PartialView("_CalendarPartial", date);
    }
}
