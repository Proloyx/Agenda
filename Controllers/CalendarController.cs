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
    private readonly User user;
    public CalendarController(ICookieService cookieService, AppDbContext context)
    {
        _cookieService = cookieService;
        _context = context;
        user = _cookieService.GetUser();
    }

    public IActionResult Index()
    {
        var userRet = _context.Users.FirstOrDefault(s => s.Userid == user.Userid);
        var calendar =new Calendar{ 
            Date = DateOnly.FromDateTime(DateTime.Now), 
            User = userRet
            }; 
        return View(calendar);
    }

    public IActionResult UpdateCalendar(DateSubmit dateSubmit) {
        var userRet = _context.Users.FirstOrDefault(s => s.Userid == user.Userid);
        var calendar = new Calendar{ 
            Date = new DateOnly(dateSubmit.Years, dateSubmit.Months, 1),
            User = userRet
            };
        return PartialView("_CalendarPartial", calendar);
    }

    public IActionResult GetScheduleByDate(int day, int month, int year) {
        var date = new DateOnly(year,month,day);
        var schedule = _context.Users.Find(user.Userid).Workcenters.SelectMany(w => w.Schedules).FirstOrDefault(s => s.Workdate == date);
        return PartialView("_ScheduleCardPartial", schedule);
    }
}
