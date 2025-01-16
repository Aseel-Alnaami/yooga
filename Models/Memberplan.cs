using System;
using System.Collections.Generic;

namespace yogago.Models;

public partial class Memberplan
{
    public decimal Memberplanid { get; set; }

    public decimal? Memberid { get; set; }

    public decimal? Planid { get; set; }

    public DateTime? Startdate { get; set; }

    public virtual ICollection<Invoiceinfo> Invoiceinfos { get; set; } = new List<Invoiceinfo>();

    public virtual Member? Member { get; set; }

    public virtual Plan? Plan { get; set; }
}
