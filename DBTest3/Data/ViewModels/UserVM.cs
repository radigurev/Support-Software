using DBTest3.Data.Entity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBTest3.Data.ViewModels
{
    public class UserVM : IdentityUser
    {
        public string? EGN { get; set; }

        public bool IsClient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ForeignKey(nameof(Company))]
        public long? CompanyId { get; set; }
        public CompanyVM? Company { get; set; }

        public ICollection<TicketsVM> Tickets { get; set; }
    }
}
