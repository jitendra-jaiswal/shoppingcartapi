using System;
using System.Collections.Generic;

namespace ShoppingCart.Infrastructure;

public partial class Product
{
    public int Id { get; set; }

    public string ProductCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Category { get; set; }

    public bool IsActive { get; set; }

    public string? Properties { get; set; }

    public decimal? UnitPrice { get; set; }

    public virtual ProductCategory CategoryNavigation { get; set; } = null!;
}
