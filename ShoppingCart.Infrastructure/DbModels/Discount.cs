namespace ShoppingCart.Infrastructure;

public partial class Discount
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Type { get; set; }

    public int DiscountDetails { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ExpiryDate { get; set; }
}
