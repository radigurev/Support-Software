using DBTest3.Config;
using DBTest3.Data;
using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DBTest3.Service
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext applicationDbContext;

        public ApplicationUserService(UserManager<User> userManager, ApplicationDbContext applicationDbContext)
        {
            this.userManager = userManager;
            this.applicationDbContext = applicationDbContext;
        }

        public async Task createUserAdmin()
        {
            if (!userManager.Users.Any())
            {
                User user = new User();

                user.Email = "radi.gurev@rado-development.eu";

                user.UserName = "Admin";

                user.UserName = "radi.gurev@rado-development.eu";

                var company = applicationDbContext.companies.Where(x => x.name.Equals("Admin")).First();

                user.CompanyId = company.Id;

                var res = await userManager.CreateAsync(user, "aA@1111");

            }
        }

        public async Task<List<UserVM>> getAllUsers()
        {
            return await userManager.Users.Include(x => x.Company).To<UserVM>().ToListAsync();
        }
    }
}
