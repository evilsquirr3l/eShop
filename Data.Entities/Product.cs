using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Data.Entities
{
    public class Product :BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public ICollection<Category> Category { get; set; }  

        public string Description { get; set; }
    }
}