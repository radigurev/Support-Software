using System;
using DBTest3.Data.ViewModels;
using DBTest3.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DBTest3.Pages.Custom
{
    public partial class ProjectData
    {

        private EditContext editContext;
        ProjectsVM project;

        List<string> errorMessages = new List<string>();

        [Parameter]
        public string Token { get; set; }

        [Inject]
        public IProjectService projectService { get; set; }
        [Inject]
        public ICompanyService companyService { get; set; }

        List<CompanyVM> companies = new List<CompanyVM>();

        protected override async Task OnInitializedAsync()
        {

            if (Token.ToLower() == "new")
                project = new ProjectsVM();
            else
                project = projectService.GetProjectById(long.Parse(Token));

            companies = this.companyService.getAllCompanies();

            this.editContext = new EditContext(project);
            this.editContext.EnableDataAnnotationsValidation();
        }

        private void Submit()
        {
            try
            {
                var validate = editContext.Validate();
                if (validate)
                {
                    if (project.Id != 0)
                    {
                        project = projectService.UpdateProject(project);
                    }
                    else
                        project = projectService.CreateProject(project);
                }else
                {
                    errorMessages.AddRange(editContext.GetValidationMessages());
                }
            }
            catch (Exception e) { }
        }
    }
}

