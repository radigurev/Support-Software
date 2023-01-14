using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBTest3.Data.Entity
{
    public class User : IdentityUser
    {
        public string? EGN { get; set; }

        public bool IsClient { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }  

        [ForeignKey(nameof(Company))]
        public long? CompanyId { get; set; }
        public Companies? Company { get; set; }

        public ICollection<Tickets> Tickets { get; set; }
    }
}
