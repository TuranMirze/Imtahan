using System.ComponentModel.DataAnnotations;

namespace Imtahan.ViewModel.Category
{
    public class CategoryCreateVM
    {
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }
        [MinLength(5)]
        public string Description { get; set; }
    }
}
