using Microsoft.AspNetCore.Components;
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

        public string NumberForContact { get; set; }

        public string ProjectLeaderName { get; set; }

        [ForeignKey(nameof(Company))]
        public long? IdCompany { get; set; }
        public Companies Company { get; set; }
    }
}

