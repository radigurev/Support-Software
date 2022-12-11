using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBTest3.Data.Entity
{
    public class User : IdentityUser
    {
        public string? EGN { get; set; }

        [ForeignKey(nameof(Location))]
        public int? IdLocation { get; set; }
        public Location Location { get; set; }
    }
}
