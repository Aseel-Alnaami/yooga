using System;
using System.Collections.Generic;

namespace yogago.Models;

public partial class Class
{
    public decimal Classid { get; set; }

    public string Classname { get; set; } = null!;

    public string Classdays { get; set; } = null!;

    public DateTime Classtime { get; set; }

    public string? Imgcover { get; set; } 

    public decimal? Categoryid { get; set; }

    public decimal? Trainerid { get; set; }

    public virtual Classcategory? Category { get; set; }

    public virtual ICollection<Classmember> Classmembers { get; set; } = new List<Classmember>();

    public virtual Trainer? Trainer { get; set; }
}
