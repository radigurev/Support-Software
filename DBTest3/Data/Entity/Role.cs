using Microsoft.AspNetCore.Identity;

namespace DBTest3.Data.Entity
{
    public class Role : IdentityRole
    {

        public string BGName { get; set; }
    }
}
