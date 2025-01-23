using System.ComponentModel.DataAnnotations;

namespace Imtahan.Models
{
    public class Product : BaseEntity
    {
        [Required]
        [MinLength(2), MaxLength(15)]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public decimal Salary { get; set; }
        public string  CoverFile { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
