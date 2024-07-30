using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Data;
using Agenda.Interfaces;
using Agenda.Models.WorkCenterModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Agenda.Controllers
{
    public class WorkCenterController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly ICookieService _cookieService;
        private readonly User user;

        public WorkCenterController(IMapper mapper,AppDbContext context , ICookieService cookieService)
        {
            _mapper = mapper;
            _context = context;
            _cookieService = cookieService;
            user = _cookieService.GetUser();
        }

        public async Task<IActionResult> Index()
        {
            var workcenters = _context.Workcenters.Where(w => w.Userid == user.Userid);
            return workcenters != null ? 
                        View(await workcenters.ToListAsync()) :
                        Problem("Entity set 'SiscomContext.RangoFirmas'  is null.");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,Grossrate,Paymentday,Netrate")] WorkCenterCreate workCenter)
        {
            if (ModelState.IsValid){
                workCenter.Userid = user.Userid;
                await _context.AddAsync(_mapper.Map<Workcenter>(workCenter));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
            return View(workCenter);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Workcenters == null)
            {
                return NotFound();
            }

            var workcenter = await _context.Workcenters.FindAsync(id);
            if (workcenter == null)
            {
                return NotFound();
            }
            
            return View(_mapper.Map<WorkCenterCreate>(workcenter));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Address,Grossrate,Paymentday,Netrate,IsActive")] WorkCenterCreate workCenter)
        {
            var center = await _context.Workcenters.FirstOrDefaultAsync(w => w.Centerid == id);
            if (center == null)
            {
                return NotFound();
            }

            center.Name = workCenter.Name;
            center.Address = workCenter.Address;
            center.Grossrate = workCenter.Grossrate;
            center.Paymentday = workCenter.Paymentday;
            center.Netrate = workCenter.Netrate;
            center.IsActive = workCenter.IsActive;

            if (ModelState.IsValid)
            {
                _context.Workcenters.Update(center);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(center);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var workcenter = await _context.Workcenters.FindAsync(id);
            if (workcenter != null)
            {
                _context.Workcenters.Remove(workcenter);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}