namespace Data.Entities;

public class CartItem : BaseEntity
{
    public virtual User User { get; set; }

    public int UserId { get; set; }

    public int Quantity { get; set; }

    public virtual Product Product { get; set; }

    public int ProductId { get; set; }
}