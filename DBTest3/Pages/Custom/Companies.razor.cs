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
            try
            {
                currentCompany = this.companyService.getCompanyById(id);

            }catch(Exception e) { }
        }

        public void updateCompanies()
        {
            try
            {
                companies = companyService.getAllCompanies();
            }catch(Exception e) { }
        }

        public void deleteCompanies(CompanyVM company)
        {
            try
            {
                this.companyService.deleteCompany(company);
                updateCompanies();
                currentCompany = new CompanyVM();
            }catch(Exception e) { }
        }
    }
}
