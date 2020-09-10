using System.Collections.Generic;

namespace Business.Models
{
    public class CartDTO:BaseDTO<int>
    {
        public ICollection<ProductDTO> Products { get; set; }

        public decimal TotalPrice { get; set; }
    }
}