using DBTest3.Data.ViewModels;

namespace DBTest3.Service
{
    public interface ICompanyService
    {
        Task<CompanyVM> CrateCompanyAsync(string name);
        CompanyVM CrateCompany(string name);
        void deleteCompany(CompanyVM company);
        List<CompanyVM> getAllCompanies();
        CompanyVM getCompanyById(long id);
        bool hasAny();
        void UpdateCompany(CompanyVM currentCompany);
    }
}
