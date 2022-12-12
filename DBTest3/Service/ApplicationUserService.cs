using DBTest3.Data.Entity;
using Microsoft.AspNetCore.Identity;

namespace DBTest3.Service
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<User> userManager;


        public ApplicationUserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task createUserAdmin()
        {
          if(!userManager.Users.Any())
            {
                User user = new User();

                user.Email = "radi.gurev@rado-development.eu";

                user.UserName = "Admin";

                user.UserName = "radi.gurev@rado-development.eu";

              var res = await userManager.CreateAsync(user,"aA@1111");
                
            }
        }
    }
}
