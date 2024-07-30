using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Models.DashboardModels;

namespace Agenda.Interfaces
{
    public interface IDashboardService
    {
        public Task<decimal> GetTotal();
        public Task<Chart> GetChart();
    }
}