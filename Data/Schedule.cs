using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Agenda.Data;

public partial class Schedule
{
    public int Scheduleid { get; set; }

    public int? Userid { get; set; }

    public int? Centerid { get; set; }

    public DateOnly Workdate { get; set; }

    public TimeOnly Starttime { get; set; }

    public TimeOnly Endtime { get; set; }
    [JsonIgnore]
    public virtual Workcenter? Center { get; set; }

    public virtual ICollection<Scheduledetail> Scheduledetails { get; set; } = new List<Scheduledetail>();
    [JsonIgnore]
    public virtual User? User { get; set; }
}
