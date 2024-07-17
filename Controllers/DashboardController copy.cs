using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Agenda.Models;
using Agenda.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Agenda.Services;

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
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = _context.Users?.FirstOrDefault().Name ?? HttpContext.TraceIdentifier });
    }
}
