using Microsoft.AspNetCore.Mvc;

namespace yogago.Models
{
   
        public class TestInfo
        {
            public Testimonial Test { get; set; } // Represents data from the Testimonial table
            public Member Member { get; set; }   // Represents data from the Member table
            public Userinfo Userinfo { get; set; } // Represents data from the UserInfo table
        }

    
}
