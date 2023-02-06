using System;
using DBTest3.Data.ViewModels;
using DBTest3.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen.Blazor;

namespace DBTest3.Pages.Custom
{
	public partial class Projects
	{
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }
        [Inject]
        IProjectService projectService { get; set; }
        [Inject]
        IApplicationUserService applicationUserService { get; set; }

        private RadzenDataGrid<ProjectsVM> grid;

        private List<ProjectsVM> projects;

        private string role;

        private UserVM CurrentUser;

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask;
            var user = authState.User;
            
            CurrentUser = await this.applicationUserService.getUserByEmail(user.Identity.Name);

            role = await this.applicationUserService.getUserRole(CurrentUser);

            UpdateTable();
        }

        private void delete(ProjectsVM project)
        {
            try
            {
                projectService.deleteProject(project);
                UpdateTable();
            }
            catch(Exception e) { }
        }


        private void UpdateTable()
        {
            if (role == "Admin")
                projects = projectService.getAllProjects();
            else
                projects = projectService.getProjectsByCompanyId(CurrentUser.CompanyId);
        }
    }
}

