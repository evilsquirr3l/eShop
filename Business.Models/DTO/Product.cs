namespace Business.Models.DTO
{
    public class Product: Base<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}