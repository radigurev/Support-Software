//using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;
using DBTest3.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
namespace DBTest3.Pages.Custom
{
    public partial class UserProfile
    {

        private UserVM CurrentUser = new UserVM();
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        IApplicationUserService applicationUserService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask;
            var user = authState.User;

            CurrentUser = await this.applicationUserService.getUserByEmail(user.Identity.Name);
        }

    }
}
