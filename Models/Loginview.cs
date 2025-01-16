using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace yogago.Models
{
    public class Loginview 
    {
        [Required(ErrorMessage = "The Username field is required.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }

}

