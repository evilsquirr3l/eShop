namespace Business.Models
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public CartDto Cart { get; set; }
    }
}