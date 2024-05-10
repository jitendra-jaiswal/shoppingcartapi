using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain;
using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;

namespace ShoppingCart.Business.DiscountStrategies
{
    public class Percentage_DiscountStrategy : IDiscountCoupon
    {
        public string DiscountCode { get; set; }
        public string ProductCode { get; set; }
        public int Limit { get; set; }
        public string Special { get; set; }
        public int? MinQuantity { get; set; }
        public string OnItemCode { get; set; }
        public int PercentageDiscount { get; set; }
        public Percentage_DiscountStrategy(DiscountModel discount)
        {
            this.ProductCode = discount.discountDetail.ProductCode;
            this.Limit = discount.discountDetail.LimitCheckout ?? 0;
            this.Special = discount.discountDetail.Special;
            this.MinQuantity = discount.discountDetail.MinimumQuantity ?? 0;
            this.OnItemCode = discount.discountDetail.OnItem ?? discount.discountDetail.ProductCode;
            this.PercentageDiscount = discount.discountDetail.PercentageDiscount ?? 0;
            this.DiscountCode = discount.Name;
        }

        public bool IsEligible(CartItemModel cartItem)
        {
            return cartItem.ProductCode == this.ProductCode && cartItem.Quantity >= this.MinQuantity;
        }

        public DiscountResult CalculateDiscount(CartItemModel cartItem, CartModel cart)
        {
            DiscountResult result = new()
            {
                ProductCode = this.ProductCode
            };
            var discountedCartItem = cart.CartItems.FirstOrDefault(x => x.ProductCode == this.OnItemCode);
            if (discountedCartItem == null)
            {
                result.DiscountCode = this.DiscountCode;
                result.DiscountAmount = 0;
                result.SpecialMessage = this.Special;
            }
            else
            {

                result.DiscountAmount = Limit == 0 ? discountedCartItem.UnitPrice * (this.PercentageDiscount * (decimal)0.01) * discountedCartItem.Quantity : discountedCartItem.UnitPrice * (this.PercentageDiscount * (decimal)0.01) * Limit;
                result.DiscountCode = this.DiscountCode;
                result.OnProductCode = this.OnItemCode;
            }
            return result;
        }
    }
}
