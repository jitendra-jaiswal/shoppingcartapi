using System;
using System.Collections.Generic;

namespace ShoppingCart.Infrastructure;

public partial class Cart
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string ProductId { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal? Discount { get; set; }

    public string? DiscountCode { get; set; }
}
