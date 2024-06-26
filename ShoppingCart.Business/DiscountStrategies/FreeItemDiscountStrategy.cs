﻿using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain;
using ShoppingCart.Domain.Responses;

namespace ShoppingCart.Business.DiscountStrategies
{
    public class FreeItemDiscountStrategy : IDiscountCoupon
    {
        public string DiscountCode { get; set; }
        public string ProductCode { get; set; }
        public int Limit { get; set; }
        public string Special { get; set; }
        public int MinQuantity { get; set; }
        public string FreeProductCode { get; set; }
        public FreeItemDiscountStrategy(DiscountModel discount)
        {
            this.ProductCode = discount.discountDetail.ProductCode;
            this.Limit = discount.discountDetail.LimitCheckout ?? 0;
            this.Special = discount.discountDetail.Special ?? "";
            this.MinQuantity = discount.discountDetail.MinimumQuantity ?? 0;
            this.FreeProductCode = discount.discountDetail.FreeItem;
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
            var freeCartItem = cart.CartItems.FirstOrDefault(x => x.ProductCode == this.FreeProductCode);
            if (freeCartItem == null)
            {
                result.DiscountCode = this.DiscountCode;
                result.DiscountAmount = 0;
                result.SpecialMessage = this.Special;
            }
            else
            {
                result.DiscountAmount = freeCartItem.UnitPrice * Limit;
                result.DiscountCode = this.DiscountCode;
                result.OnProductCode = this.FreeProductCode;
            }
            return result;
        }
    }
}
