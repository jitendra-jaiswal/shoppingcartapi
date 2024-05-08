using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain;
using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Business.DiscountStrategies
{
    public class Percentage_DiscountStrategy: IDiscountCoupon
    {
        public string DiscountCode { get; set; }
        public string ProductCode { get; set; }
        public int Limit { get; set; }
        public string Special { get; set; }
        public int? MinQuantity { get; set; }
        public string OnItemCode { get; set; }
        public int PercentageDiscount { get; set; }
        public Percentage_DiscountStrategy(Discount discount)
        {
            this.ProductCode = discount.DiscountDetailsNavigation.ProductCode;
            this.Limit = discount.DiscountDetailsNavigation.LimitCheckout ?? 0;
            this.Special = discount.DiscountDetailsNavigation.Special;
            this.MinQuantity = discount.DiscountDetailsNavigation.MinimumQuantity ?? 0;
            this.OnItemCode = discount.DiscountDetailsNavigation.OnItem ?? discount.DiscountDetailsNavigation.ProductCode;
            this.PercentageDiscount = discount.DiscountDetailsNavigation.PercentageDiscount ?? 0;
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
                
                result.DiscountAmount = Limit == 0? discountedCartItem.UnitPrice * (this.PercentageDiscount*(decimal)0.01)*discountedCartItem.Quantity: discountedCartItem.UnitPrice * (this.PercentageDiscount *(decimal)0.01) * Limit;
                result.DiscountCode = this.DiscountCode;
                result.OnProductCode = this.OnItemCode;
            }
            return result;
        }
    }
}
