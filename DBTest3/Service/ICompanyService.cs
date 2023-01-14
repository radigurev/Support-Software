namespace DBTest3.Service
{
    public interface ICompanyService
    {
        Task CrateCompany(string name);
        bool hasAny();
    }
}
