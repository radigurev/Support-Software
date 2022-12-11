using DBTest3.Data.Entity;

namespace DBTest3.Service
{
    public class ApplicationUserService : IApplicationUserService
    {
        public async Task createUserAdmin()
        {
            User user = new User();

            user.Email = "radi.gurev@rado-development.eu";

            user.UserName = "Admin";

        }
    }
}
