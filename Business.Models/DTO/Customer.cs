namespace Business.Models.DTO
{
    public class Customer:Base<int>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public Cart Cart { get; set; }
    }
}