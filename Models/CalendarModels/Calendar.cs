using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Agenda.Data;

namespace Agenda.Models.CalendarModels
{
    public class Calendar
    {
        public DateOnly Date {get; set;}
        public User User {get; set;}
    }
}