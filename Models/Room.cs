using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Room
{
    public int Roomid { get; set; }

    public string? Roomnumber { get; set; }

    public string? Roomtype { get; set; }

    public int? Capacity { get; set; }

    public decimal? Pricepernight { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Roomamenity> Roomamenities { get; set; } = new List<Roomamenity>();

    public virtual ICollection<Roomservice> Roomservices { get; set; } = new List<Roomservice>();
}
