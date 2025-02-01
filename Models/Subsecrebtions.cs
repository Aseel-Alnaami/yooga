using Microsoft.AspNetCore.Mvc;

using yogago.Models;

namespace yogago.Models
{
    public class Subsecrebtions
    {

        public Memberplan Memberplan { get; set; } // Represents data from the Testimonial table
        public Member Member { get; set; }   // Represents data from the Member table
        public  Plan Plan { get; set; }
        public Userinfo Userinfo   { get; set; }

    }
}
