namespace ShoppingCart.Infrastructure;

public partial class Discount
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Type { get; set; }

    public string? DetailsJson { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public virtual DiscountType TypeNavigation { get; set; } = null!;
}
