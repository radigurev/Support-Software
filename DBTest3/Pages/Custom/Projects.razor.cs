using System;
using DBTest3.Data.ViewModels;
using DBTest3.Service;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace DBTest3.Pages.Custom
{
	public partial class Projects
	{
        [Inject]
        IProjectService projectService { get; set; }

        private RadzenDataGrid<ProjectsVM> grid;

        private List<ProjectsVM> projects;
        protected override async Task OnInitializedAsync()
        {
            projects = projectService.getAllProjects();
        }

        private void delete(ProjectsVM project)
        {
            projectService.deleteProject(project);
        }
    }
}

