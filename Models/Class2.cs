using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace yogago.Models
{
    public class Class2 { 
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



}
}
