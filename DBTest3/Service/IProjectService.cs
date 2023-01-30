using System;
using DBTest3.Data.ViewModels;

namespace DBTest3.Service
{
    public interface IProjectService
    {
        void deleteProject(ProjectsVM project);
        List<ProjectsVM> getAllProjects();
    }
}

