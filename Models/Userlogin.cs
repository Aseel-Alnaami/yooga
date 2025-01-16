using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace yogago.Models;

public partial class Userlogin
{
    public decimal Loginid { get; set; }

    public decimal? Userid { get; set; }


	public string Username { get; set; } = null!;

	//[DataType(DataType.Password)]

	public string Password { get; set; } = null!;

    public decimal? Roleid { get; set; }

    public virtual Role? Role { get; set; }

    public virtual Userinfo? User { get; set; }
}
