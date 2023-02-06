using DBTest3.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBTest3.Data.ViewModels
{
    public class ProjectsVM
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Полето 'Име' е задължително!")]
        public string name { get; set; }
        [Required(ErrorMessage = "Полето 'Тип' е задължително!")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Полето 'Номер' е задължително!")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Полето 'ЕИК' може да съдържа само цифри!")]
        public string NumberForContact { get; set; }

        [Required(ErrorMessage = "Полето 'Лице' е задължително!")]
        public string ProjectLeaderName { get; set; }

        [Required(ErrorMessage = "Полето 'Фирма' е задължително!")]
        [ForeignKey(nameof(Company))]
        public long? IdCompany { get; set; }
        public Companies Company { get; set; }
    }
}
