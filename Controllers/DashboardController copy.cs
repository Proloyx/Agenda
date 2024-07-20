using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Agenda.Models;
using Agenda.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Agenda.Services;
using Agenda.Models.DashboardModels;
using Agenda.Interfaces;

namespace Agenda.Controllers;

[Authorize]
[TypeFilter(typeof(ValidateUserFilter))]
public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;
    private readonly AppDbContext _context;
    private readonly ICookieService _cookieService;
    private readonly User user;

    public DashboardController(ILogger<DashboardController> logger, AppDbContext context, ICookieService cookieService)
    {
        _logger = logger;
        _context = context;
        _cookieService = cookieService;
        user = _cookieService.GetUser();
    }

    public async Task<IActionResult> Index()
    {
        var sum = await _context.Users.Where(u => u.Userid == user.Userid)
                .SelectMany(u => u.Workcenters)
                .SelectMany(s => s.Schedules)
                .SumAsync(u => u.Workedhours * u.Center.Netrate);

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
