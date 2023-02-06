using System;
using DBTest3.Data.ViewModels;

namespace DBTest3.Service
{
    public interface IProjectService
    {
        ProjectsVM CreateProject(ProjectsVM project);
        void deleteProject(ProjectsVM project);
        List<ProjectsVM> getAllProjects();
        ProjectsVM GetProjectById(long v);
        List<ProjectsVM> getProjectsByCompanyId(long? companyId);
        ProjectsVM UpdateProject(ProjectsVM project);
    }
}

