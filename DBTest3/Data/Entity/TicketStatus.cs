using System;
using System.ComponentModel.DataAnnotations;

namespace DBTest3.Data.Entity
{
	public class TicketStatus
	{
		public TicketStatus()
		{
		}

        [Key]
        public long Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string NameBG { get; set; }

		//За теглене от база данни
		[Required]
		public string Code { get; set; }

		public virtual ICollection<Tickets> Tickets { get; set; }
    }
}

