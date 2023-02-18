using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBTest3.Data.Entity
{
	public class Chat
	{
        [Key]
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
        public Tickets Ticket { get; set; }
    }
}

