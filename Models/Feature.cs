using System.ComponentModel.DataAnnotations;

namespace hwapp.Models
{
    public class Feature
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}