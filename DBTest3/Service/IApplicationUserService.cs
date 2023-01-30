using DBTest3.Data.ViewModels;

namespace DBTest3.Service
{
    public interface IApplicationUserService
    {
        Task<UserVM> createUser(UserVM currentUser);
        Task createUserAdmin();
        Task deleteUserAsync(UserVM user);
        Task<List<UserVM>> getAllUsers();
        Task<UserVM> getUserByEmail(string name);
        Task<UserVM> getUserById(string id);
        Task<string> getUserRole(UserVM currentUser);
        void updateUser(UserVM currentUser);
    }
}
