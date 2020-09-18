using System.Collections.Generic;

namespace Business.Models
{
    public class CartDto
    {
        public int Id { get; set; }

        public ICollection<ProductDto> Products { get; set; }

        public decimal TotalPrice { get; set; }
    }
}