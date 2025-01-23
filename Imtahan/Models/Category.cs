using System.ComponentModel.DataAnnotations;

namespace Imtahan.Models
{
    public class Category :BaseEntity
    {
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }
        [MinLength(5)]
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
