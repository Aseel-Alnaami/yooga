using System;
using System.Collections.Generic;

namespace yogago.Models;

public partial class Homemanagementtext
{
    public decimal Textid { get; set; }

    public string Textname { get; set; } = null!;

    public string Content { get; set; } = null!;
}
