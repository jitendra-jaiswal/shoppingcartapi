using ShoppingCart.Domain;
using ShoppingCart.Domain.Requests;
using ShoppingCart.Domain.Responses;

namespace ShoppingCart.Business.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountModel> GetDiscounts(int id);
        Task<IEnumerable<DiscountModel>> GetAllActiveDiscounts(bool includeType = true);
        Task<List<IDiscountCoupon>> GetAllDiscountCoupons();
        Task ApplyDiscounts(CartModel cart, List<IDiscountCoupon> coupons);
        bool DisableDiscount(int id);
        bool DeleteDiscount(int id);
        bool AddDiscount(AddDiscountModel model);
    }
}
