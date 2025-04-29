using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Payment
{
    public int Paymentid { get; set; }

    public int Bookingid { get; set; }

    public DateOnly Paymentdate { get; set; }

    public decimal? Amount { get; set; }

    public string? Paymentmethod { get; set; }

    public string? Paymentstatus { get; set; }

    public string? Transactionnumber { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
