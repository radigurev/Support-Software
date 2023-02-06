using DBTest3.Data.ViewModels;

namespace DBTest3.Service
{
    public interface IApplicationRoleService
    {
        string getRoleById(string? role);
        List<RoleVM> getRoles();
        Task initRoles();
    }
}
