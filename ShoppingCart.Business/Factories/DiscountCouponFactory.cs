﻿using ShoppingCart.Business.DiscountStrategies;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain;
using ShoppingCart.Domain.enums;

namespace ShoppingCart.Business.Factories
{
    public interface IDiscountCouponFactory
    {
        List<IDiscountCoupon> BuildDiscountCoupons(IEnumerable<DiscountModel> discounts);
    }
    public class DiscountCouponFactory : IDiscountCouponFactory
    {
        List<IDiscountCoupon> coupons = new();
        public List<IDiscountCoupon> BuildDiscountCoupons(IEnumerable<DiscountModel> discounts)
        {
            foreach (DiscountModel discount in discounts)
            {
                var discountStrategy = CreateCoupon(discount);
                if (discountStrategy != null)
                {
                    coupons.Add(discountStrategy);
                }
            }
            return coupons;
        }
        private IDiscountCoupon CreateCoupon(DiscountModel discount)
        {
            switch ((DiscountTypes)discount.Type)
            {
                case DiscountTypes.BOGO:
                    return new BOGO_DiscountStrategy(discount);
                case DiscountTypes.FreeItem:
                    return new FreeItemDiscountStrategy(discount);
                case DiscountTypes.PercentageDiscount:
                    return new Percentage_DiscountStrategy(discount);
                case DiscountTypes.PriceDrop:
                    return new PriceDrop_DiscountStrategy(discount);
            }
            return null;
        }
    }
}
