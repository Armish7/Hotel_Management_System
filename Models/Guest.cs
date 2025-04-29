using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Guest
{
    public int Guestid { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Email { get; set; }

    public string? Phonenumber { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
