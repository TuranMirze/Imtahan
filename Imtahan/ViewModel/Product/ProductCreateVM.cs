namespace Imtahan.ViewModel.Product
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Salary { get; set; }
        public IFormFile CoverFile { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; internal set; }
    }
}
