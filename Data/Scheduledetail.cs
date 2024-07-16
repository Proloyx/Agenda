using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Agenda.Data;

public partial class Scheduledetail
{
    public int Scheduledetailid { get; set; }

    public int? Scheduleid { get; set; }

    public TimeOnly Starttime { get; set; }

    public TimeOnly Endtime { get; set; }
    [JsonIgnore]
    public virtual Schedule? Schedule { get; set; }
}
