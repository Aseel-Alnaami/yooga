using System;
using System.Collections.Generic;

namespace yogago.Models;

public partial class Discount
{
    public decimal Discountid { get; set; }

    public string Discountname { get; set; } = null!;

    public string Discountcode { get; set; } = null!;

    public decimal Discountpercentage { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime? Enddate { get; set; }

    public string? Isactive { get; set; }
}
