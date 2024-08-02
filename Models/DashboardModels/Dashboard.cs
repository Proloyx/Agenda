using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Data;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Models.DashboardModels
{
    public class Dashboard
    {
        public User User {get; set;}
        public string Chart {get; set;}
        public decimal AverageWorkedHours {get; set;}
        public decimal AverageMonthWorkedHours {get; set;}
        public decimal AverageMonthGrossRate{get; set;}
        public decimal AverageMonthNetRate{get; set;}
        public decimal TotalWorkedHours{get; set;}
    }
}