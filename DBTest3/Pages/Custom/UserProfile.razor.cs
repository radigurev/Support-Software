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
                    }
                    else
                       CurrentUser = await applicationUserService.getUserById(Id);
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
        }

    }
}
