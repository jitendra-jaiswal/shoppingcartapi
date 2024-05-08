namespace ShoppingCart.Infrastructure;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CartItem> Carts { get; set; } = new List<CartItem>();
}
