using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBTest3.Data.Entity
{
	public class Projects
	{
		public Projects()
		{
		}

        [Key]
        public long Id { get; set; }

        public string name { get; set; }

		public string Type { get; set; }

        [ForeignKey(nameof(Project))]
        public long ProjectId { get; set; }
		public Projects Project { get; set; }
    }
}

