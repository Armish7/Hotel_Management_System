using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Roomservice
{
    public int Serviceid { get; set; }

    public int Roomid { get; set; }

    public int Employeeid { get; set; }

    public DateOnly Servicedate { get; set; }

    public string? Servicetype { get; set; }

    public string? Status { get; set; }

    public string? Servicedetails { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Room Room { get; set; } = null!;
}
