using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yogago.Models;

public partial class Contactu
{
    public decimal Contactid { get; set; }

    [Required]

    public string Name { get; set; } = null!;
   
    [Required, EmailAddress]

    public string Email { get; set; } = null!;
    [Required]

    public string Subject { get; set; } = null!;
    [Required]

    public string Message { get; set; } = null!;

    public DateTime? Submitteddate { get; set; }
    
}
