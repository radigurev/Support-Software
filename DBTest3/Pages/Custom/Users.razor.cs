using DBTest3.Data.ViewModels;
using DBTest3.Service;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace DBTest3.Pages.Custom
{
    public partial class Users
    {

        private RadzenDataGrid<UserVM> grid;

        private List<UserVM> users;

        [Inject]
        private IApplicationUserService applicationUserService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            users = await applicationUserService.getAllUsers();
        }
    }
}
