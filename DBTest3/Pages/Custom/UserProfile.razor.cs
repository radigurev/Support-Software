//using DBTest3.Data.Entity;
using DBTest3.Config;
using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;
using DBTest3.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
namespace DBTest3.Pages.Custom
{
    public partial class UserProfile
    {

        private UserVM CurrentUser = new UserVM();
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        IApplicationUserService applicationUserService { get; set; }
        [Inject]
        ICompanyService companyService { get; set; }
        [Inject]
        IApplicationRoleService applicationRoleService { get; set; }

        List<string> errorMessages = new List<string>();

        List<RoleVM> roles;

        [Parameter]
        public string Id { get; set; }

        EditContext editContext;

        public List<CompanyVM> companies;

        protected override async Task OnInitializedAsync()
        {

            companies = companyService.getAllCompanies();

          try
            {
                if (string.IsNullOrEmpty(Id))
                {
                   
                        var authState = await authenticationStateTask;
                        var user = authState.User;

                        CurrentUser = await this.applicationUserService.getUserByEmail(user.Identity.Name);
                    
                }
                else
                {
                    if (Id.Equals("New"))
                    {
                        CurrentUser = new UserVM();
                        CurrentUser.Company = new CompanyVM();
                        roles = applicationRoleService.getRoles();
                    }
                    else
                    {
                        CurrentUser = await applicationUserService.getUserById(Id);
                        CurrentUser.role = await applicationUserService.getUserRole(CurrentUser); 
                    }
                }
            }
            catch(Exception e)
            {

            }
            editContext = new EditContext(CurrentUser);
            editContext.EnableDataAnnotationsValidation();
        }

        public void Submit()
        {
            var validate = this.editContext.Validate();


            if (validate)
            {
                var adminCompany = companies.Where(x => x.name.Equals("Admin")).First();
                if (CurrentUser.role == "Admin" && CurrentUser.CompanyId != adminCompany.Id)
                {
                    errorMessages.Add("Админ или проверител трябва да имат компания 'Админ' !");
                }
                else
                {
                    if (string.IsNullOrEmpty(CurrentUser.Id))
                        applicationUserService.createUser(CurrentUser);
                    else
                        applicationUserService.updateUser(CurrentUser);
                }
            }
        }

    }
}
