using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Booking
{
    public int Bookingid { get; set; }

    public int Guestid { get; set; }

    public int Roomid { get; set; }

    public DateOnly Checkin { get; set; }

    public DateOnly Checkout { get; set; }

    public DateOnly? Bookingdate { get; set; }

    public string? Status { get; set; }

    public virtual Guest Guest { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Room Room { get; set; } = null!;
}
