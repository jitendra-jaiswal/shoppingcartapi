using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain;
using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;

namespace ShoppingCart.Business.DiscountStrategies
{
    public class BOGO_DiscountStrategy : IDiscountCoupon
    {
        public string DiscountCode { get; set; }
        public string ProductCode { get; set; }
        public string OnProductCode { get; set; }
        public int? Limit { get; set; }
        public string Special { get; set; }
        public BOGO_DiscountStrategy(Discount discount)
        {
            this.ProductCode = discount.DiscountDetailsNavigation.ProductCode;
            this.Limit = discount.DiscountDetailsNavigation.LimitCheckout;
            this.Special = discount.DiscountDetailsNavigation.Special;
            this.DiscountCode = discount.Name;
            this.OnProductCode = this.OnProductCode;
        }

        public bool IsEligible(CartItemModel cartItem)
        {
            return cartItem.ProductCode == this.ProductCode;
        }

        public DiscountResult CalculateDiscount(CartItemModel cartItem, CartModel cart)
        {
            DiscountResult result = new()
            {
                ProductCode = this.ProductCode
            };
            if (cartItem.Quantity == 1)
            {
                result.DiscountCode = this.DiscountCode;
                result.DiscountAmount = 0;
                result.SpecialMessage = this.Special;
            }
            else
            {
                var bogoquantity = cartItem.Quantity / 2;
                result.DiscountAmount = cartItem.UnitPrice * bogoquantity;
                result.DiscountCode = this.DiscountCode;
                result.OnProductCode = this.ProductCode;
            }
            return result;
        }
    }
}
