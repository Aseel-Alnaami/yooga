using System;
using System.Collections.Generic;

namespace yogago.Models;

public partial class Classcategory
{
    public decimal Categoryid { get; set; }

    public string Categoryname { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
