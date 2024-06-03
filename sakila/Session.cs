using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CDHB_Official.sakila;

public partial class Session
{
    public int Id { get; set; }

    [DisplayName("Session Name")]
    public string? Name { get; set; } = null!;

    [DisplayName("Staff Id")]
    public int StaffId { get; set; }

    [DisplayName("Subspecialty Id")]
    public int SubspecialtyId { get; set; }

    [DisplayName("Acute")]
    public bool IsAcute { get; set; }

    [DisplayName("Pediatric")]
    public bool IsPediatric { get; set; }

    [DisplayName("Anaesthetic Type")]
    public string? AnaestheticType { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual Staff? Staff { get; set; } = null!;

    public virtual Subspecialty? Subspecialty { get; set; } = null!;
}
