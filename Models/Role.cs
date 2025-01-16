using System;
using System.Collections.Generic;

namespace yogago.Models;

public partial class Role
{
    public decimal Roleid { get; set; }

    public string Rolename { get; set; } = null!;

    public virtual ICollection<Userinfo> Userinfos { get; set; } = new List<Userinfo>();

    public virtual ICollection<Userlogin> Userlogins { get; set; } = new List<Userlogin>();
}
