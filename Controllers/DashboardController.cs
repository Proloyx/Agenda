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
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Npgsql;

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
                Total = await _dashboardService.GetTotal(),
                User = await _context.Users.FindAsync(user.Userid)
            };
        
        return View(dash);
    }

    [HttpGet]
    public async Task<IActionResult> GetChartData()
    {
        var chart = await _dashboardService.GetChart();
        return Json(chart);
    }
}
