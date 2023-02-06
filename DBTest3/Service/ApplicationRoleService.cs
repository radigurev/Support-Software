using DBTest3.Config;
using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;
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

        public string getRoleById(string? role)
        {
            return roleManager.FindByIdAsync(role).Result.Name;
        }

        public List<RoleVM> getRoles()
        {
            return roleManager.Roles.To<RoleVM>().ToList();
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
