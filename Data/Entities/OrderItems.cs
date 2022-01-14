namespace Data.Entities;

public class OrderItems : BaseEntity
{
    public virtual OrderDetails OrderDetails { get; set; }

    public int OrderDetailsId { get; set; }

    public virtual Product Product { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
}