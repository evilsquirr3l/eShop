namespace Database.Entities;

public class CartItem : BaseEntity
{
    public User User { get; set; }

    public int UserId { get; set; }

    public int Quantity { get; set; }

    public Product Product { get; set; }

    public int ProductId { get; set; }
}