using DBTest3.Data.ViewModels;
using DBTest3.Service;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using System;

namespace DBTest3.Pages.Custom
{
    public partial class Companies
    {
        private RadzenDataGrid<CompanyVM> grid;

        private List<CompanyVM> companies;

        private CompanyVM currentCompany;

        [Inject]
        private ICompanyService companyService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            updateCompanies();

            currentCompany = new CompanyVM();
        }

        void OnChange(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
            {
                currentCompany = new CompanyVM();
            }
        }

        public void selectRow(long id)
        {
            currentCompany = this.companyService.getCompanyById(id);
        }

        public void Update()
        {
            this.companyService.UpdateCompany(currentCompany);
            updateCompanies();
        }

        public void Create()
        {
           currentCompany = this.companyService.CrateCompany(currentCompany.name);
            updateCompanies();
        }

        public void updateCompanies()
        {
            companies = companyService.getAllCompanies();
        }

        public void deleteCompanies(CompanyVM company)
        {
            this.companyService.deleteCompany(company);
            updateCompanies();
            currentCompany = new CompanyVM();
        }
    }
}
