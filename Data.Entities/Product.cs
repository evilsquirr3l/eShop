using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Product : BaseEntity
    {
        [Required] 
        public string Name { get; set; }

        [Required] 
        public decimal Price { get; set; }

        [Required] 
        public string ImageUrl { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }
    }
}