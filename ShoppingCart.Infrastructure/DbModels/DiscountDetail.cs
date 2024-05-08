namespace ShoppingCart.Infrastructure;

public partial class DiscountDetail
{
    public int Id { get; set; }

    public string? ProductCode { get; set; }

    public int? CategoryCode { get; set; }

    public int? PercentageDiscount { get; set; }

    public decimal? FixedDiscount { get; set; }

    public decimal? FixedPrice { get; set; }

    public decimal? MaxDiscount { get; set; }

    public string? FreeItem { get; set; }

    public int? MinimumQuantity { get; set; }

    public string? OnItem { get; set; }

    public string? Condition { get; set; }

    public int? LimitCheckout { get; set; }

    public int? LimitforPeriod { get; set; }

    public string? Special { get; set; }
}
