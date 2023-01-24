using DBTest3.Data.Entity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBTest3.Data.ViewModels
{
    public class UserVM : IdentityUser
    {
        [Required(ErrorMessage = "Полето ЕГН е задължително!")]
        public string? EGN { get; set; }

        [Required(ErrorMessage = "Полето Email е задължително!")]
        public override string Email { get => base.Email; set => base.Email = value; }

        public bool IsClient { get; set; }
        [Required(ErrorMessage = "Полето Име е задължително!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Полето Фамилия е задължително!")]
        public string LastName { get; set; }

        [ForeignKey(nameof(Company))]
        [Required(ErrorMessage = "Полето Фирма е задължително!")]
        public long? CompanyId { get; set; }
        public CompanyVM? Company { get; set; }

        public ICollection<TicketsVM> Tickets { get; set; }
    }
}
