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

        public async Task<decimal> GetTotal(){
            var sum = await _context.Users.Where(u => u.Userid == user.Userid)
                .SelectMany(u => u.Workcenters)
                .SelectMany(s => s.Schedules)
                .SumAsync(u => u.Workedhours * u.Center.Netrate);
            return sum;
        }

        public async Task<Chart> GetChart(){
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
    }
}