using System;
using System.Collections.Generic;

namespace yogago.Models;

public partial class Classmember
{
    public decimal Classmemberid { get; set; }

    public decimal? Classid { get; set; }

    public decimal? Memberid { get; set; }

    public virtual Class? Class { get; set; }

    public virtual Member? Member { get; set; }
}
