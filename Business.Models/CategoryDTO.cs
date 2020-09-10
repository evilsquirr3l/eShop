namespace Business.Models
{
    public class CategoryDTO:BaseDTO<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}