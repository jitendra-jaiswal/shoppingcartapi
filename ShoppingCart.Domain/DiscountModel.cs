using ShoppingCart.Infrastructure;

namespace ShoppingCart.Domain
{
    public class DiscountModel : Discount
    {
        public DiscountDetail discountDetail { get; set; }
    }
}
