using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Agenda.Data;

public partial class Schedule
{
    public int Scheduleid { get; set; }

    public int Centerid { get; set; }
    [Display(Name = "Fecha")]
    public DateOnly Workdate { get; set; }
    [Display(Name = "Entrada")]
    public TimeOnly Starttime { get; set; }
    [Display(Name = "Salida")]
    public TimeOnly Endtime { get; set; }
    [Display(Name = "Horas")]
    public decimal Workedhours { get; set; }
    [Display(Name = "Descripción")]
    public string? Description { get; set; }
    [JsonIgnore]
    public virtual Workcenter Center { get; set; } = null!;
}
