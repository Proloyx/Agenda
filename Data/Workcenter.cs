using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Agenda.Data;

public partial class Workcenter
{
    public int Centerid { get; set; }

    public int Userid { get; set; }
    [Display(Name = "Nombre")]
    public string Name { get; set; } = null!;

    public string? Address { get; set; }
    [Display(Name = "Bruto")]
    public decimal Grossrate { get; set; }
    [Display(Name = "Día de Cobro")]
    public int? Paymentday { get; set; }
    [Display(Name = "Neto")]
    public decimal Netrate { get; set; }
    [Display(Name = "En activo")]
    public bool IsActive { get; set; }
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
    
}
