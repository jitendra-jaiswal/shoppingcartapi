using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain;
using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;

namespace ShoppingCart.Business.DiscountStrategies
{
    public class PriceDrop_DiscountStrategy : IDiscountCoupon
    {
        public string DiscountCode { get; set; }
        public string ProductCode { get; set; }
        public string Special { get; set; }
        public int MinQuantity { get; set; }
        public string OnItemCode { get; set; }
        public decimal? FixedPrice { get; set; }
        public PriceDrop_DiscountStrategy(Discount discount)
        {
            this.ProductCode = discount.DiscountDetailsNavigation.ProductCode;
            this.Special = discount.DiscountDetailsNavigation.Special;
            this.MinQuantity = discount.DiscountDetailsNavigation.MinimumQuantity ?? 0;
            this.OnItemCode = discount.DiscountDetailsNavigation.ProductCode;
            this.FixedPrice = discount.DiscountDetailsNavigation.FixedPrice;
            this.DiscountCode = discount.Name;
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
            if (cartItem.Quantity < this.MinQuantity)
            {
                result.DiscountCode = this.DiscountCode;
                result.DiscountAmount = 0;
                result.SpecialMessage = this.Special;
            }
            else
            {
                result.DiscountAmount = this.FixedPrice == null ? 0 : (cartItem.UnitPrice - this.FixedPrice.Value) * cartItem.Quantity;
                result.DiscountCode = this.DiscountCode;
                result.OnProductCode = this.ProductCode;
            }
            return result;
        }
    }
}
