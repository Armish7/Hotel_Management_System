using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Employee
{
    public int Employeeid { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Jobtitle { get; set; }

    public string? Phonenumber { get; set; }

    public string? Email { get; set; }

    public DateOnly? Hiredate { get; set; }

    public virtual ICollection<Roomservice> Roomservices { get; set; } = new List<Roomservice>();
}
