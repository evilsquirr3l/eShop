using System.Collections.Generic;

namespace Business.Models.DTO
{
    public class Cart:Base<int>
    {
        public ICollection<Product> Products { get; set; }

        public decimal TotalPrice { get; set; }
    }
}