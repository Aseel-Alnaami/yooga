using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace yogago.Models;

public partial class Class
{
    public decimal Classid { get; set; }

    public string Classname { get; set; } = null!;

    public string Classdays { get; set; } = null!;

    public DateTime Classtime { get; set; }

    [NotMapped]
    public virtual IFormFile? Imgcoverfile { get; set; }// For file upload

    public string? Imgcover { get; set; }

    public decimal? Categoryid { get; set; }

    public decimal? Trainerid { get; set; }

    public virtual Classcategory? Category { get; set; }

    public virtual ICollection<Classmember> Classmembers { get; set; } = new List<Classmember>();

    public virtual Trainer? Trainer { get; set; }



}