using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Category : BaseEntity
    {
        [Required] 
        public string Name { get; set; }

        [Required] 
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}