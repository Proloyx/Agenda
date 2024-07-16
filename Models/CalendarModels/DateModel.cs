using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Models.CalendarModels
{
    public class DateModel
    {
        public DateOnly Date {get; set;} = DateOnly.FromDateTime(DateTime.Now);
    }
}