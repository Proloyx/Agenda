using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Agenda.Models.ScheduleModels;

public partial class ScheduleCreate
{

    public int? Userid { get; set; }

    public int? Centerid { get; set; }

    public DateOnly Workdate { get; set; }

    public TimeOnly Starttime { get; set; }

    public TimeOnly Endtime { get; set; }
    [Range(1, 10, ErrorMessage = "Paymentday must be a number between 1 and 10.")]
    public int Workedhours { get; set; }

    public string? Description { get; set; }
}
