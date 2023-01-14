using DBTest3.Data.ViewModels;

namespace DBTest3.Service
{
    public interface IApplicationUserService
    {
        Task createUserAdmin();
        Task<List<UserVM>> getAllUsers();
    }
}
