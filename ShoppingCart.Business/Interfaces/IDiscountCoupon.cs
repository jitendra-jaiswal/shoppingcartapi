using ShoppingCart.Domain;
using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;

namespace ShoppingCart.Business.Interfaces
{
    public interface IDiscountCoupon
    {
        bool IsEligible(CartItemModel cartItem);
        DiscountResult CalculateDiscount(CartItemModel cartItem, CartModel cart);
    }
}
