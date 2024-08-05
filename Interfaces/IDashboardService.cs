using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Models.DashboardModels;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Interfaces
{
    public interface IDashboardService
    {
        public Task<Chart> GetChart();
        public Task<decimal> GetAverageWorkedHours();
        public Task<decimal> GetMonthAverageTotalWorkedHours();
        public Task<decimal> GetMonthAverageGrossRate();
        public Task<decimal> GetMonthAverageNetRate();
    }
}