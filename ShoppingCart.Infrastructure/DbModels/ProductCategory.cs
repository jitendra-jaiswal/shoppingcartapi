﻿namespace ShoppingCart.Infrastructure;

public partial class ProductCategory
{
    public int CategoryCode { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
