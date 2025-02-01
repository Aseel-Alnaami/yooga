using System;
using System.Collections.Generic;

namespace yogago.Models;

public partial class Invoiceinfo
{
    public decimal Invoiceid { get; set; }

    public decimal? Memberplanid { get; set; }

    public decimal Amount { get; set; }

    public DateTime? Generateddate { get; set; }

    public bool Emailsent { get; set; } = false;

    public virtual Memberplan? Memberplan { get; set; }
}
