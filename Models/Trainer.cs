using System;
using System.Collections.Generic;

namespace yogago.Models;

public partial class Trainer
{
    public decimal Trainerid { get; set; }

    public decimal? Userid { get; set; }

    public string? Specialization { get; set; }

    public decimal? Experience { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Userinfo? User { get; set; }
}
