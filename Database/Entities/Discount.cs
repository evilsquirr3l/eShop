namespace Database.Entities;

public class Discount : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal DiscountPercent { get; set; }

    public bool IsActive { get; set; }
}