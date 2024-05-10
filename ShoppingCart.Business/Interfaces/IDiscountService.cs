using ShoppingCart.Domain;
using ShoppingCart.Domain.Responses;

namespace ShoppingCart.Business.Interfaces
{
    public interface IDiscountService
    {
        Task<IEnumerable<DiscountModel>> GetAllActiveDiscounts();
        Task<List<IDiscountCoupon>> GetAllDiscountCoupons();
        Task ApplyDiscounts(CartModel cart, List<IDiscountCoupon> coupons);
    }
}
