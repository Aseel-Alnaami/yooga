using System;
using System.Collections.Generic;

namespace yogago.Models;

public partial class Plan
{
    public decimal Planid { get; set; }

    public string Planname { get; set; } = null!;

    public int Durationmonths { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Memberplan> Memberplans { get; set; } = new List<Memberplan>();
}
