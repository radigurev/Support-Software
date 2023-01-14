using DBTest3.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBTest3.Data.ViewModels
{
    public class ProjectsVM
    {
        public long Id { get; set; }

        public string name { get; set; }

        public string Type { get; set; }

        [ForeignKey(nameof(Company))]
        public long? IdCompany { get; set; }
        public Companies Company { get; set; }
    }
}
