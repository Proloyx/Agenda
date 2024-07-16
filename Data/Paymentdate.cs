using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Agenda.Data;

public partial class Paymentdate
{
    public int Paymentdateid { get; set; }

    public int? Centerid { get; set; }

    public DateOnly Paymentdate1 { get; set; }
    [JsonIgnore]
    public virtual Workcenter? Center { get; set; }
}
