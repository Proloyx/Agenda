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
    public CalendarController(ICookieService cookieService)
    {
        _cookieService = cookieService;
    }

    public IActionResult Index()
    {
        var calendar =new Calendar{ 
            Date = DateOnly.FromDateTime(DateTime.Now), 
            User = _cookieService.GetUser()
            }; 
        return View(calendar);
    }

    public IActionResult UpdateCalendar(DateSubmit dateSubmit) {
        var calendar = new Calendar{ 
            Date = new DateOnly(dateSubmit.Years, dateSubmit.Months, 1),
            User = _cookieService.GetUser()
            };
        return View("index", calendar);
        //return PartialView("_CalendarPartial", date);
    }
}
