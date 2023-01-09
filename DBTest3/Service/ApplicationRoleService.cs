using DBTest3.Data.Entity;
using Microsoft.AspNetCore.Identity;

namespace DBTest3.Service
{
    public class ApplicationRoleService : IApplicationRoleService
    {

        private readonly RoleManager<Role> roleManager;

        public ApplicationRoleService(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task initRoles()
        {
            if (!roleManager.Roles.Any())
            {
                List<Role> roles = new List<Role>()
               {
                 new Role()
                {
                    BGName = "Потребител",
                    Name = "User"
                },
                    new Role()
                {
                    BGName = "Админ",
                    Name = "Admin"
                },
                       new Role()
                {
                    BGName = "Проверител",
                    Name = "Validator"
                }
            };
                try
                {
                    foreach (var role in roles)
                    {
                        await roleManager.CreateAsync(role);
                    }
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
