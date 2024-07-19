using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Models.WorkCenterModels
{
    public class WorkCenterCreate
    {
        public int? Userid { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public decimal Grossrate { get; set; }
        [Range(1, 31, ErrorMessage = "Paymentday must be a number between 1 and 31.")]
        public int? Paymentday { get; set; }
        public decimal Netrate { get; set; }
    }
}