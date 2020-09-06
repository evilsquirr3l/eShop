using System.ComponentModel.DataAnnotations;
namespace Data.Entities
{
    public class Category : BaseEntity<int>
    {
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}