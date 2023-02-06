using DBTest3.Config;
using DBTest3.Data;
using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DBTest3.Service
{
    public class ApplicationUserService : IApplicationUserService
    {
        static RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IApplicationRoleService applicationRoleService;
        private readonly IMailService mailService;
        public ApplicationUserService(UserManager<User> userManager, ApplicationDbContext applicationDbContext, IMailService mailService, IApplicationRoleService applicationRoleService)
        {
            this.userManager = userManager;
            this.applicationDbContext = applicationDbContext;
            this.mailService = mailService;
            this.applicationRoleService = applicationRoleService;
        }

        public async Task<UserVM> createUser(UserVM currentUser)
        {
            var user = currentUser.To<User>();

            try
            {
                var password = CreatePassword(10);

                user.UserName = user.Email;

                user.Company = null;

               var res = await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user, applicationRoleService.getRoleById(currentUser.role));

                mailService.SendNewUserMail(password, user.Email);
            }catch(Exception e) { }

            return user.To<UserVM>();
        }

        public async Task createUserAdmin()
        {
            if (!userManager.Users.Any())
            {
                User user = new User();

                user.Email = "radi.gurev@rado-development.eu";

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
            applicationDbContext.ChangeTracker.Clear();
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

        public async Task<bool> hasRole(UserVM user,string role)
        {
            return (await getUserRole(user)).Equals(role);
        }

        public void updateUser(UserVM currentUser)
        {
            this.applicationDbContext.Update(currentUser.To<User>());
            this.applicationDbContext.SaveChanges();
        }

        private string CreatePassword(int length)
        {
            string characterSet =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
        "abcdefghijklmnopqrstuvwxyz" +
        "0123456789";

            if (length < 0)
                throw new ArgumentException("length must not be negative", "length");
            if (length > int.MaxValue / 8)
                throw new ArgumentException("length is too big", "length");
            if (characterSet == null)
                throw new ArgumentNullException("characterSet");
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
                throw new ArgumentException("characterSet must not be empty", "characterSet");

            var bytes = new byte[length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }
    }
}
