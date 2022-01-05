namespace Database.Entities;

public class OrderItems : BaseEntity
{
    public OrderDetails OrderDetails { get; set; }

    public int OrderId { get; set; }

    public Product Product { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
}