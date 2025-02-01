using System;
using System.Collections.Generic;

namespace yogago.Models;

public partial class Paymentinfo
{
    public decimal Cardid { get; set; }

    public decimal? Memberid { get; set; }

    public string Fullname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Zipcode { get; set; } = null!;

    public string Cardnumberencrypted { get; set; } = null!;

    public string Cvvencrypted { get; set; } = null!;

    public int Expirymonth { get; set; }

    public int Expiryyear { get; set; }

    public DateTime? Createddate { get; set; }

    public virtual Member? Member { get; set; }
}
//public int Balance { get; set; }
