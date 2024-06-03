using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CDHB_Official.sakila;

public partial class Schedule
{
    public int Id { get; set; }

    [ForeignKey("Theatre")]
    [DisplayName("Theatre Id")]
    public int TheatreId { get; set; }

    [ForeignKey("Session")]
    [DisplayName("Session Id")]
    public int SessionId { get; set; }

    [Range(0, 53, ErrorMessage = "Please enter valid week number")]
    public int Week { get; set; }

    [DisplayName("AM/PM")]
    public bool IsAm { get; set; }

    public string? Day { get; set; } = null!;

    [DisplayName("Start Time")]
    public TimeSpan TimeStart { get; set; }

    [DisplayName("End Time")]
    public TimeSpan TimeEnd { get; set; }

    public virtual Session? Session { get; set; } = null!;

    public virtual Theatre? Theatre { get; set; } = null!;
}
