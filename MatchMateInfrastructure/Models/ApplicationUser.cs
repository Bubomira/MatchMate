using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMateInfrastructure.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public DateTime Birthday { get; set; }
        public IList<UserInterest> UsersInterests { get; set; } = new List<UserInterest>();

    }
}
