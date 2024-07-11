using System;
using System.Collections.Generic;

namespace Agenda.Data;

public partial class Workcenter
{
    public int Centerid { get; set; }

    public int? Userid { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public decimal Grossrate { get; set; }

    public decimal Netrate { get; set; }

    public virtual ICollection<Paymentdate> Paymentdates { get; set; } = new List<Paymentdate>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual User? User { get; set; }
}
