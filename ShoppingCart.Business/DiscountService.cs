using Microsoft.Extensions.Logging;
using ShoppingCart.Business.Factories;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain;
using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;

namespace ShoppingCart.Business
{
    public class DiscountService : IDiscountService
    {
        private readonly ILogger<DiscountService> _logger;
        private readonly IRepository<Discount> _discountRepository;
        private readonly ICacheService _cache;
        private readonly IDiscountCouponFactory _couponFactory;
        public DiscountService(ILogger<DiscountService> logger, IRepository<Discount> discountRepository, ICacheService cache, IDiscountCouponFactory couponFactory)
        {
            _logger = logger;
            _discountRepository= discountRepository;
            _cache = cache;
            _couponFactory = couponFactory;
        }

        public IEnumerable<Discount> GetAllActiveDiscounts()
        {
            DateTime today = DateTime.Now;
            var discounts = _discountRepository.GetAll(x => x.IsActive == true && today >= x.CreatedDate && today <= x.ExpiryDate, new List<System.Linq.Expressions.Expression<Func<Discount, object>>> { x=> x.DiscountDetailsNavigation, x=> x.TypeNavigation});
            return discounts;
        }

        public List<IDiscountCoupon> GetAllDiscountCoupons()
        {
            var coupons = _cache.GetDiscountCoupons();
            if (coupons != null)
                return coupons;

            var allactiveDiscounts = GetAllActiveDiscounts();
            if (allactiveDiscounts == null)
                return null;
            var discountCoupons = _couponFactory.BuildDiscountCoupons(allactiveDiscounts);
            if (discountCoupons != null)
                _cache.SetDiscountCoupons(discountCoupons);

            return discountCoupons;
        }

        public void ApplyDiscounts(CartModel cart, List<IDiscountCoupon> coupons)
        {
            List<DiscountResult> discountResults = new();
            foreach (var item in cart.CartItems)
            {
                DiscountResult discountResult = ProceessDiscountforItem(cart, coupons, item);
                if (discountResult != null)
                    discountResults.Add(discountResult);
            }

            foreach (var discount in discountResults)
            {
                AppyDiscountonCartItems(cart, discount);
            }            
        }
        private void AppyDiscountonCartItems(CartModel cart, DiscountResult discount)
        {
            if (discount.DiscountAmount == 0)
            {
                var cartItem = cart.CartItems.First(x => x.ProductCode == discount.ProductCode);
                cartItem.SpecialMessage = discount.SpecialMessage;
            }
            else
            {
                var cartItem = cart.CartItems.First(x => x.ProductCode == discount.OnProductCode);
                if (cartItem.Discount != null && cartItem.Discount > 0 && cartItem.Discount < discount.DiscountAmount)
                {
                    cartItem.Discount = discount.DiscountAmount;
                    cartItem.DiscountCode = discount.DiscountCode;
                }
                else
                {
                    cartItem.Discount = discount.DiscountAmount;
                    cartItem.DiscountCode = discount.DiscountCode;
                }
            }
        }

        private DiscountResult ProceessDiscountforItem(CartModel cart, List<IDiscountCoupon> coupons, CartItemModel item)
        {
            DiscountResult discountResult = null;
            var eligibleCoupons = coupons.Where(x => x.IsEligible(item));
            if (eligibleCoupons == null)
                return discountResult;

            foreach (var coupon in eligibleCoupons)
            {
                var discount = coupon.CalculateDiscount(item, cart);
                if (discountResult == null)
                {
                    discountResult = discount;
                }
                else
                {
                    discountResult = discountResult.DiscountAmount > discount.DiscountAmount ? discountResult : discount;
                }
            }
            return discountResult;
        }
    }
}
