using System.Collections.Generic;

namespace Business.Models
{
    public class CategoryDTO:BaseDTO<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductDTO> Products { get; set; }
    }
}