using System;
using System.Collections.Generic;

namespace Agenda.Data;

public partial class Paymentdate
{
    public int Paymentdateid { get; set; }

    public int? Centerid { get; set; }

    public DateOnly Paymentdate1 { get; set; }

    public virtual Workcenter? Center { get; set; }
}
