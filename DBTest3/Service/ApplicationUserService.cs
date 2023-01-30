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

        public async Task<UserVM> createUser(UserVM currentUser)
        {
            var user = currentUser.To<User>();
            await userManager.CreateAsync(user);

            await userManager.AddToRoleAsync(user, currentUser.role);

            return user.To<UserVM>();
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

                user.FirstName = "Lidiya";

                user.LastName = "Vicheva";

                var res = await userManager.CreateAsync(user, "aA@1111");

                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        public async Task deleteUserAsync(UserVM user)
        {
            await this.userManager.DeleteAsync(user.To<User>());
        }

        public async Task<List<UserVM>> getAllUsers()
        {
            return await userManager.Users.Include(x => x.Company).To<UserVM>().ToListAsync();
        }

        public async Task<UserVM> getUserByEmail(string email)
        {
            var user = userManager.Users.Where(x => x.Email == email).Include(x => x.Company).First();
            return user.To<UserVM>();
        }

        public async Task<UserVM> getUserById(string id)
        {
            var user = userManager.Users.Include(x => x.Company).Where(x => x.Id == id).First();
            return user.To<UserVM>();
        }

        public async Task<string> getUserRole(UserVM currentUser)
        {
            var roleUser = applicationDbContext.UserRoles.Where(x => x.UserId == currentUser.Id).First();
            var role = applicationDbContext.roles.Where(x => x.Id == roleUser.RoleId).First();
            return role.Name;
        }

        public void updateUser(UserVM currentUser)
        {
            this.applicationDbContext.Update(currentUser.To<User>());
            this.applicationDbContext.SaveChanges();
        }
    }
}
