using System;
using DBTest3.Config;
using DBTest3.Data;
using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;

namespace DBTest3.Service
{
    public class ProjectService : IProjectService
    {
        private ApplicationDbContext applicationDbContext;

        public ProjectService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void deleteProject(ProjectsVM project)
        {
            applicationDbContext.Remove(project.To<Projects>());
            applicationDbContext.SaveChanges();
        }

        public List<ProjectsVM> getAllProjects()
        {
            return applicationDbContext.projects.To<ProjectsVM>().ToList();
        }
    }
}

