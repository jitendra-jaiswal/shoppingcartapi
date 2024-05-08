using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;

namespace ShoppingCart.Business.Interfaces
{
    public interface IDiscountService
    {
        IEnumerable<Discount> GetAllActiveDiscounts();
        List<IDiscountCoupon> GetAllDiscountCoupons();
        void ApplyDiscounts(CartModel cart, List<IDiscountCoupon> coupons);
    }
}
