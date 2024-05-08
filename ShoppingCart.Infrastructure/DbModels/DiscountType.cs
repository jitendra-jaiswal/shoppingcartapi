namespace ShoppingCart.Infrastructure;

public partial class DiscountType
{
    public int Id { get; set; }

    public string DiscountType1 { get; set; } = null!;

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}
