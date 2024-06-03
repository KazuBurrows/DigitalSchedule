using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CDHB_Official.sakila;

public partial class Staff
{
    public int Id { get; set; }

    [DisplayName("First Name")]
    public string? FirstName { get; set; }

    [DisplayName("Last Name")]
    public string? LastName { get; set; }

    public string? Code { get; set; } = null!;

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
