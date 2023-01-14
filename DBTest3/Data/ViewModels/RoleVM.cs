using Microsoft.AspNetCore.Identity;

namespace DBTest3.Data.ViewModels
{
    public class RoleVM : IdentityRole
    {
        public string BGName { get; set; }

    }
}
