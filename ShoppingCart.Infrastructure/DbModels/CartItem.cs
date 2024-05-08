namespace ShoppingCart.Infrastructure;

public partial class CartItem
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string ProductId { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public virtual User User { get; set; } = null!;
}
