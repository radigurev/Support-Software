using DBTest3.Data.Entity;

namespace DBTest3.Data.ViewModels
{
    public class CompanyVM
    {
        public long Id { get; set; }

        public string name { get; set; }

        public virtual ICollection<Projects> Projects { get; set; }
    }
}
