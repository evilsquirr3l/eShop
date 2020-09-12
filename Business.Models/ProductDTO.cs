using System.ComponentModel;

namespace Business.Models
{
    public class ProductDTO: BaseDTO<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public CategoryDTO Category { get; set; }
    }
}