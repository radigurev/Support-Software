using DBTest3.Config;
using DBTest3.Data;
using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;
using System.Linq.Dynamic.Core;

namespace DBTest3.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CompanyService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public Task CrateCompany(string name)
        {
            var company = new CompanyVM()
            {
                name = name
            };

            applicationDbContext.companies.Add(company.To<Companies>());
            applicationDbContext.SaveChanges();

            return Task.CompletedTask;
        }

        public bool hasAny()
        {
           return applicationDbContext.companies.Any();
        }
    }
}
