using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Roomamenity
{
    public int Roomamenityid { get; set; }

    public int Roomid { get; set; }

    public int Amenityid { get; set; }

    public DateOnly? Assigneddate { get; set; }

    public virtual Hotelamenity Amenity { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
