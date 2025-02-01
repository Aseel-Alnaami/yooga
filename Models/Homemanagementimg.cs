using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace yogago.Models;

public partial class Homemanagementimg
{
    public decimal Imgid { get; set; }

    public string Imgname { get; set; } = null!;

    public string Imgpath { get; set; } = null!;


    [NotMapped]
    public virtual IFormFile? Imgfile { get; set; }
}
