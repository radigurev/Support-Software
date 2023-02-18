using DBTest3.Config;
using DBTest3.Data;
using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
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

        public CompanyVM CrateCompany(CompanyVM company)
        {
            var companyDB = company.To<Companies>();
            applicationDbContext.companies.Add(companyDB);
             applicationDbContext.SaveChanges();

            applicationDbContext.ChangeTracker.Clear();

            return companyDB.To<CompanyVM>();
        }

        public async Task<CompanyVM> CrateCompanyAsync(string name)
        {
            var company = new CompanyVM()
            {
                name = name,
                EIK = "1234567890",
                ManagerName = "Lidiya Vicheva",
                address = "Rayko Aleksiev 28"
            };
            var c = company.To<Companies>();
            applicationDbContext.companies.Add(c);
            await applicationDbContext.SaveChangesAsync();

            return c.To<CompanyVM>();
        }

        public void deleteCompany(CompanyVM company)
        {
            applicationDbContext.ChangeTracker.Clear();

            this.applicationDbContext.Remove(company.To<Companies>());
            this.applicationDbContext.SaveChanges();
        }

        public List<CompanyVM> getAllCompanies()
        {
            var list = applicationDbContext.companies.AsNoTracking().To<CompanyVM>().ToList();

            return list;
        }

        public CompanyVM getCompanyById(long id)
        {
            return this.applicationDbContext
                .companies
                .AsNoTracking()
                .Where(x => x.Id == id)
                .First()
                .To<CompanyVM>();
        }

        public bool hasAny()
        {
           return applicationDbContext.companies.Any();
        }

        public void UpdateCompany(CompanyVM currentCompany)
        {
            this.applicationDbContext.Update(currentCompany.To<Companies>());
            this.applicationDbContext.SaveChanges();

            this.applicationDbContext.ChangeTracker.Clear();
        }
    }
}
