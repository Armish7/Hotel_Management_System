using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Hotelamenity
{
    public int Amenityid { get; set; }

    public string? Amenityname { get; set; }

    public string? Description { get; set; }

    public string? Availabilitystatus { get; set; }

    public virtual ICollection<Roomamenity> Roomamenities { get; set; } = new List<Roomamenity>();
}
