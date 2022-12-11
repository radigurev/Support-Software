using System.ComponentModel.DataAnnotations;

namespace DBTest3.Data.Entity
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255, ErrorMessage = "The Name value cannot exceed 255 characters. ")]
        public string Name { get; set; }
    }
}
