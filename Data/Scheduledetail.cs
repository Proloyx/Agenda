using System;
using System.Collections.Generic;

namespace Agenda.Data;

public partial class Scheduledetail
{
    public int Scheduledetailid { get; set; }

    public int? Scheduleid { get; set; }

    public TimeOnly Starttime { get; set; }

    public TimeOnly Endtime { get; set; }

    public virtual Schedule? Schedule { get; set; }
}
