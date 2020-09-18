using System.Collections.Generic;

namespace Data.Entities
{
    public class Cart : BaseEntity
    {
        public ICollection<Product> Products { get; set; }

        public decimal TotalPrice { get; set; }
    }
}