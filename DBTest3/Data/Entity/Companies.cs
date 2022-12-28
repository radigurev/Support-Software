using System;
using System.ComponentModel.DataAnnotations;

namespace DBTest3.Data.Entity
{
	public class Companies
	{
		public Companies()
		{
		}

		[Key]
		public long Id { get; set; }

		public string name { get; set; }

        public virtual ICollection<Projects> Projects { get; set; }
    }
}

