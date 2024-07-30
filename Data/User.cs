using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;

namespace Agenda.Data;

public partial class User
{
    public int Userid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Workcenter> Workcenters { get; set; } = new List<Workcenter>();

    public string GetWorkCenters(){
        string workCenters = "";
        foreach (var workCenter in this.Workcenters.Where(w => w.IsActive == true)){
            workCenters += workCenter.Name + ", ";
        };
        return workCenters;
    }
}
