using System.ComponentModel.DataAnnotations;
namespace Data.Entities
{
    public class Customer : BaseEntity<int>
    {
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public Cart Cart { get; set; }
    }
}