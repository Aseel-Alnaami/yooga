using System;
using System.Collections.Generic;

namespace yogago.Models;

public partial class Member
{
    public decimal Memberid { get; set; }

    public decimal? Userid { get; set; }

    public DateTime? Joindate { get; set; }

    public virtual ICollection<Classmember> Classmembers { get; set; } = new List<Classmember>();

    public virtual ICollection<Memberplan> Memberplans { get; set; } = new List<Memberplan>();

    public virtual ICollection<Paymentinfo> Paymentinfos { get; set; } = new List<Paymentinfo>();

    public virtual ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();

    public virtual Userinfo? User { get; set; }
}
