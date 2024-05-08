using System;
using System.Collections.Generic;

namespace ShoppingCart.Infrastructure;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;
}
