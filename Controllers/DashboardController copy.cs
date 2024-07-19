using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Agenda.Models;
using Agenda.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Agenda.Services;
using Agenda.Models.DashboardModels;

namespace Agenda.Controllers;

[Authorize]
[TypeFilter(typeof(ValidateUserFilter))]
public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;
    private readonly AppDbContext _context;

    public DashboardController(ILogger<DashboardController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var date = DateOnly.FromDateTime(DateTime.Now);
        var schedules = _context.Schedules.Where(s => s.Workdate.Month == date.Month && s.Workdate.Year == date.Year);
        decimal sum = 0;
        foreach (var item in schedules)
        {
            sum += item.Center.Netrate * item.Workedhours;
        }
        Dashboard dash = new Dashboard{
            Total = sum
        }; 
        return View(dash);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = _context.Users?.FirstOrDefault().Name ?? HttpContext.TraceIdentifier });
    }
}
