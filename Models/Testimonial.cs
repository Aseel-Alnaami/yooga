using System;
using System.Collections.Generic;

namespace yogago.Models;

public partial class Testimonial
{
    public decimal Testimonialid { get; set; }

    public decimal? Memberid { get; set; }

    public string Content { get; set; } = null!;

    public decimal? Rating { get; set; }

    public string? Status { get; set; }

    public DateTime? Submitteddate { get; set; }

    public virtual Member? Member { get; set; }
}
