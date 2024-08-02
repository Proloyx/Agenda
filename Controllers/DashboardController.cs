using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Agenda.Models;
using Agenda.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Agenda.Services;
using Agenda.Models.DashboardModels;
using Agenda.Interfaces;
using System.Text.Json;

namespace Agenda.Controllers;

[Authorize]
[TypeFilter(typeof(ValidateUserFilter))]
public class DashboardController : Controller
{
    private readonly AppDbContext _context;
    private readonly ICookieService _cookieService;
    private readonly User user;
    private readonly IDashboardService _dashboardService;

    public DashboardController(AppDbContext context, ICookieService cookieService, IDashboardService dashboardService)
    {
        _context = context;
        _cookieService = cookieService;
        user = _cookieService.GetUser();
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index()
    {
        var dash = new Dashboard
            {
                User = await _context.Users.FindAsync(user.Userid),
                Chart = JsonSerializer.Serialize(await _dashboardService.GetChart()),
                AverageWorkedHours = await _dashboardService.GetAverageWorkedHours(),
                AverageMonthWorkedHours = await _dashboardService.GetMonthAverageWorkedHours(),
                AverageMonthGrossRate = await _dashboardService.GetMonthAverageGrossRate(),
                AverageMonthNetRate = await _dashboardService.GetMonthAverageNetRate(),
                TotalWorkedHours = await _dashboardService.GetTotalWorkedHours()
            };
        
        return View(dash);
    }
}
