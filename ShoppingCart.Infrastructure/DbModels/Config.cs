namespace ShoppingCart.Infrastructure;

public partial class Config
{
    public int Id { get; set; }
    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;
}
