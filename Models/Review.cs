using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Review
{
    public int Reviewid { get; set; }

    public int Guestid { get; set; }

    public int? Roomid { get; set; }

    public int? Serviceid { get; set; }

    public int? Rating { get; set; }

    public string? Reviewtext { get; set; }

    public DateOnly Reviewdate { get; set; }

    public string? Title { get; set; }

    public virtual Guest Guest { get; set; } = null!;

    public virtual Room? Room { get; set; }

    public virtual Roomservice? Service { get; set; }
}
