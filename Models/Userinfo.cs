using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace yogago.Models
{
    public partial class Userinfo
    {
        public decimal Userid { get; set; }

        //[Required]
        public string Fullname { get; set; } = null!;

        //[Required]
        public string Username { get; set; } = null!;

        //[Required]
        //[EmailAddress]
        public string Email { get; set; } = null!;

        //[Phone]
        public string? Phone { get; set; }

        public DateTime? Dateofbirth { get; set; }

        [Required]
        public string Address { get; set; } = null!;

        //[NotMapped]

        //public string? ProfilepicturePath { get; set; }// To store the path in the database

        [NotMapped]
        public virtual IFormFile? Profilepicturefile { get; set; }// For file upload

        public string? Profilepicture { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public decimal Roleid { get; set; }

        public DateTime? Dateadded { get; set; }

        public virtual ICollection<Member> Members { get; set; } = new List<Member>();

        public virtual Role Role { get; set; } = null!;

        public virtual ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();

        public virtual ICollection<Userlogin> Userlogins { get; set; } = new List<Userlogin>();
    }
}
