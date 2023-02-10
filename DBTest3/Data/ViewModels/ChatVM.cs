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

        [Required]
        [ForeignKey(nameof(Ticket))]
        public long TicketID { get; set; }
        public TicketsVM Ticket { get; set; }

        public virtual ICollection<ChatVM> Chats { get; set; }
    }
}

