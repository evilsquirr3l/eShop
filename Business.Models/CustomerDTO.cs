namespace Business.Models
{
    public class CustomerDTO:BaseDTO<int>
    {
        public string Email { get; set; }
        public CartDTO Cart { get; set; }
    }
}