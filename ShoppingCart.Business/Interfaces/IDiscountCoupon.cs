using ShoppingCart.Domain;
using ShoppingCart.Domain.Responses;

namespace ShoppingCart.Business.Interfaces
{
    public interface IDiscountCoupon
    {
        bool IsEligible(CartItemModel cartItem);
        DiscountResult CalculateDiscount(CartItemModel cartItem, CartModel cart);
    }
}
