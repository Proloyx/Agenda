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

        //Grafico de ultimos 12 meses
        public string Chart {get; set;}

        // Informacion Personal
        public decimal AverageWorkedHours {get; set;}
        public decimal Nuevo {get; set;}
        public decimal AverageMonthTotalWorkedHours {get; set;}
        public decimal AverageMonthGrossRate{get; set;}
        public decimal AverageMonthNetRate{get; set;}
    }
}