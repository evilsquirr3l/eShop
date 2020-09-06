using System.ComponentModel.DataAnnotations;
namespace Data.Entities
{
    public class Product : BaseEntity<int>
    {
        //TODO: M:1 with category
        //TODO: decimal price, string name, string imageUrl, string description
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}