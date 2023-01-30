using System.ComponentModel.DataAnnotations;
using DBTest3.Data.Entity;

namespace DBTest3.Data.ViewModels
{
    public class CompanyVM
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Полето 'Име' е задължително!")]
        public string name { get; set; }

        [Required(ErrorMessage = "Полето 'Еик' е задължително!")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Полето 'ЕИК' може да съдържа само цифри!")]
        public string EIK { get; set; }

        [Required(ErrorMessage = "Полето 'Адрес' е задължително!")]
        public string address { get; set; }

        [Required(ErrorMessage = "Полето 'Мениджър(Име)' е задължително!")]
        public string ManagerName { get; set; }

        public virtual ICollection<Projects> Projects { get; set; }
    }
}
