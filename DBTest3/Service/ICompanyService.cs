using DBTest3.Data.ViewModels;

namespace DBTest3.Service
{
    public interface ICompanyService
    {
        Task CrateCompany(string name);
        List<CompanyVM> getAllCompanies();
        bool hasAny();
    }
}
