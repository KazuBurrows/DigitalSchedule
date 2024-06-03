using System;
using System.Collections.Generic;

namespace CDHB_Official.sakila;

public partial class Subspecialty
{
    public int Id { get; set; }

    public string? Code { get; set; } = null!;

    public string? Speciality { get; set; } = null!;

    public string? SubSpeciality { get; set; } = null!;

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
