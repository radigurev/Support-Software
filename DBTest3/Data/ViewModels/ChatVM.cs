using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DBTest3.Data.Entity;

namespace DBTest3.Data.ViewModels
{
	public class ChatVM
	{
        public long Id { get; set; }

        [Required]
        [StringLength(5000)]
        public string Message { get; set; }

        public DateTime date { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        [ForeignKey(nameof(Ticket))]
        public long TicketID { get; set; }
        public TicketsVM Ticket { get; set; }

    }
}

