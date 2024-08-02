using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Data;
using Agenda.Interfaces;
using Agenda.Models.DashboardModels;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Agenda.Services
{
    public class DashboardService:IDashboardService
    {
        private readonly AppDbContext _context;
        private readonly ICookieService _cookieService;
        private readonly User user;
        public DashboardService(AppDbContext context, ICookieService cookieService)
        {
            _context = context;
            _cookieService = cookieService;
            user = _cookieService.GetUser();
        }

        public async Task<Chart> GetChart(){
            try
            {
                var userIdParam = new NpgsqlParameter("user_id", user.Userid);

                var data = await _context.Set<MonthlySalary>()
                           .FromSqlRaw("SELECT * FROM GetTotalSalaryForUserLast12Months(@user_id)", userIdParam)
                           .ToListAsync();
    
                var chart = new Chart
                {
                    Months = data.Select(ms => ms.Month).ToList(),
                    Totals = data.Select(ms => ms.TotalSalary).ToList()
                };
                return chart;
            }
            catch (System.Exception)
            {
                return new Chart();
            }
        }

        public async Task<decimal> GetAverageWorkedHours(){
            try
            {
                var us = await _context.Users.FindAsync(user.Userid);
                var averageWorkedhours = us.Workcenters.SelectMany(w => w.Schedules).Average(s => s.Workedhours);
                return Math.Round(averageWorkedhours, 2);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<decimal> GetMonthAverageWorkedHours(){
            try
            {
                var us = await _context.Users.FindAsync(user.Userid);
                var schedules = us.Workcenters.SelectMany(w => w.Schedules);
                var monthlyAverages = schedules
                .GroupBy(s => new { s.Workdate.Year, s.Workdate.Month })
                .Select(g => new
                {
                    Month = $"{g.Key.Year}-{g.Key.Month:D2}",
                    AverageHours = g.Average(s => s.Workedhours)
                })
                .ToDictionary(g => g.Month, g => g.AverageHours);
                var monthAverageWorkedHours = monthlyAverages.Values.Any() ? monthlyAverages.Values.Average() : 0;
                return Math.Round(monthAverageWorkedHours, 2);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<decimal> GetMonthAverageGrossRate(){
            try
            {
                var us = await _context.Users.FindAsync(user.Userid);
                var monthlyGrossSalaries = us.Workcenters
                    .SelectMany(workCenter => workCenter.Schedules.Select(schedule => new
                        {
                            schedule.Workdate.Year,
                            schedule.Workdate.Month,
                            GrossSalary = schedule.Workedhours * workCenter.Grossrate
                        }))
                    .GroupBy(x => new { x.Year, x.Month })
                    .Select(g => new
                        {
                            Month = $"{g.Key.Year}-{g.Key.Month:D2}",
                            GrossSalary = g.Sum(x => x.GrossSalary)
                        })
                    .ToDictionary(g => g.Month, g => g.GrossSalary);
                
                var monthAverageGrossRate = monthlyGrossSalaries.Values.Any() ? monthlyGrossSalaries.Values.Average() : 0;

                return Math.Round(monthAverageGrossRate, 2);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<decimal> GetMonthAverageNetRate(){
            try
            {
                var us = await _context.Users.FindAsync(user.Userid);
                var monthlyNetSalaries = us.Workcenters
                    .SelectMany(workCenter => workCenter.Schedules.Select(schedule => new
                        {
                            schedule.Workdate.Year,
                            schedule.Workdate.Month,
                            NetSalary = schedule.Workedhours * workCenter.Netrate
                        }))
                    .GroupBy(x => new { x.Year, x.Month })
                    .Select(g => new
                        {
                            Month = $"{g.Key.Year}-{g.Key.Month:D2}",
                            NetSalary = g.Sum(x => x.NetSalary)
                        })
                    .ToDictionary(g => g.Month, g => g.NetSalary);
                var monthAverageNetRate = monthlyNetSalaries.Values.Any() ? monthlyNetSalaries.Values.Average() : 0;
                return Math.Round(monthAverageNetRate, 2);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<decimal> GetTotalWorkedHours(){
            try
            {
                var us = await _context.Users.FindAsync(user.Userid);
                var totalWorkedhours = us.Workcenters.SelectMany(w => w.Schedules).Sum(s => s.Workedhours);
                return Math.Round(totalWorkedhours, 2);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}