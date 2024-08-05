using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Data;
using Agenda.Interfaces;
using Agenda.Models.ScheduleModels;
using Agenda.Models.WorkCenterModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Agenda.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly ICookieService _cookieService;
        private readonly User user;

        public ScheduleController(IMapper mapper,AppDbContext context , ICookieService cookieService)
        {
            _mapper = mapper;
            _context = context;
            _cookieService = cookieService;
            user = _cookieService.GetUser();
        }

        public async Task<IActionResult> Index()
        {
            var schedules = _context.Workcenters.Where(w => w.Userid == user.Userid ).SelectMany(w => w.Schedules);
                    return schedules != null ? 
                        View(await schedules.OrderBy(s => s.Workdate).Take(15).ToListAsync()) :
                        Problem("Entity set 'SiscomContext.RangoFirmas'  is null.");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Workdate,Starttime,Endtime,Workedhours,Description")] ScheduleCreate schedule)
        {
            if (ModelState.IsValid){
                schedule.Centerid = id;
                await _context.AddAsync(_mapper.Map<Schedule>(schedule));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
            return View(schedule);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            
            return View(_mapper.Map<ScheduleCreate>(schedule));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Workdate,Starttime,Endtime,Workedhours,Description")] ScheduleCreate scheduleCreate)
        {
            var schedule = await _context.Schedules.FirstOrDefaultAsync(w => w.Scheduleid == id);
            if (schedule == null)
            {
                return NotFound();
            }

            schedule.Workdate = scheduleCreate.Workdate;
            schedule.Starttime = scheduleCreate.Starttime;
            schedule.Endtime = scheduleCreate.Endtime;
            schedule.Workedhours = scheduleCreate.Workedhours;
            schedule.Description = scheduleCreate.Description;

            if (ModelState.IsValid)
            {
                _context.Schedules.Update(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
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