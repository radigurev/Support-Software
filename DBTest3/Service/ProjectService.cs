using System;
using DBTest3.Config;
using DBTest3.Data;
using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DBTest3.Service
{
    public class ProjectService : IProjectService
    {
        private ApplicationDbContext applicationDbContext;

        public ProjectService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public ProjectsVM CreateProject(ProjectsVM project)
        {
            var projectForDb = project.To<Projects>();

            this.applicationDbContext.Add(projectForDb);
            this.applicationDbContext.SaveChanges();

            return GetProjectById(projectForDb.Id);
        }

        public void deleteProject(ProjectsVM project)
        {
            this.applicationDbContext.Remove(project.To<Projects>());
            this.applicationDbContext.SaveChanges();
        }

        public List<ProjectsVM> getAllProjects()
        {
            return this.applicationDbContext.projects.To<ProjectsVM>().ToList();
        }

        public ProjectsVM GetProjectById(long v)
        {
            var project = this.applicationDbContext.projects.Include(x => x.Company).Where(x => x.Id == v).First();
            this.applicationDbContext.ChangeTracker.Clear();
            return project.To<ProjectsVM>();
        }

        public List<ProjectsVM> getProjectsByCompanyId(long? companyId)
        {
            return this.applicationDbContext.projects.Where(x => x.IdCompany == companyId).To<ProjectsVM>().ToList();
        }

        public ProjectsVM UpdateProject(ProjectsVM project)
        {
            this.applicationDbContext.ChangeTracker.Clear();
            project.Company = null;
            this.applicationDbContext.Update(project.To<Projects>());

            this.applicationDbContext.SaveChanges();

            project = this.GetProjectById(project.Id);

            return project;
        }
    }
}

