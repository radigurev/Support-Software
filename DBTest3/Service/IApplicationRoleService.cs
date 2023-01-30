using DBTest3.Data.ViewModels;

namespace DBTest3.Service
{
    public interface IApplicationRoleService
    {
        List<RoleVM> getRoles();
        Task initRoles();
    }
}
