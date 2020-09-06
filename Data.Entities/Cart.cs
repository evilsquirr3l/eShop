namespace Data.Entities
{
    public class Cart : BaseEntity<int>
    {
        public List<Product> Products  { get; set; }

        public decimal TotalPrice { get; set; }
        //TODO: 1 customer, 1 cart???
        //TODO: shipping address???
        //TODO: decimal total price
    }
}