using System;
using System.Collections.Generic;

namespace Agenda.Data;

public partial class User
{
    public int Userid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Workcenter> Workcenters { get; set; } = new List<Workcenter>();
}
