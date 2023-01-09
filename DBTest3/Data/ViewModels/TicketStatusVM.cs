using System.ComponentModel.DataAnnotations;

namespace DBTest3.Data.ViewModels
{
    public class TicketStatusVM
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string NameBG { get; set; }

        //За теглене от база данни
        [Required]
        public string Code { get; set; }
    }
}
