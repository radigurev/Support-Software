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

		public string Name { get; set; }
    }
}

