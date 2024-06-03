using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDHB_Official.sakila;

public partial class Theatre
{
    public int Id { get; set; }

    [DisplayName("Theatre Name")]
    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string Facility { get; set; } = null!;

    [DisplayName("Scope Theatre Code")]
    public string ScopeTheatreCode { get; set; } = null!;

    public string Equipment { get; set; } = null!;

    public string Specialties { get; set; } = null!;

    public bool Pediatric { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
