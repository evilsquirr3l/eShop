using System.Runtime.InteropServices.ComTypes;

namespace Business.Models.DTO
{
    public class Category:Base<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}