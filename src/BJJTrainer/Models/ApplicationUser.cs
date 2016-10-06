using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BJJTrainer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Affiliation { get; set; }
        public string Rank { get; set; }
        public string Location { get; set; }
        public string Bio { get; set; }
    }
}
