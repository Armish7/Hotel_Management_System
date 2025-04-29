using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Maintenance
{
    public int Maintenanceid { get; set; }

    public int Roomid { get; set; }

    public string? Issuedescription { get; set; }

    public DateOnly Requestdate { get; set; }

    public string? Status { get; set; }

    public string? Maintenancetype { get; set; }

    public virtual Room Room { get; set; } = null!;
}
