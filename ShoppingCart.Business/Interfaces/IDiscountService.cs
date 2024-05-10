using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;

namespace ShoppingCart.Business.Interfaces
{
    public interface IDiscountService
    {
        Task<IEnumerable<Discount>> GetAllActiveDiscounts();
        Task<List<IDiscountCoupon>> GetAllDiscountCoupons();
        Task ApplyDiscounts(CartModel cart, List<IDiscountCoupon> coupons);
    }
}
