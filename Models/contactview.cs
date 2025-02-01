using System.ComponentModel.DataAnnotations;

namespace yogago.Models
{
    public class contactview
    {
        public Contactu Contact { get; set; }

        [Required]
        public string Response { get; set; } = string.Empty;
    }
}
