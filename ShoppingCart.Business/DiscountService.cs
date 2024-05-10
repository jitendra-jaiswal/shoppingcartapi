using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShoppingCart.Business.Factories;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain;
using ShoppingCart.Domain.Requests;
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
        private readonly IRepository<Config> _configRepository;
        private readonly IMapper _mapper;
        public DiscountService(ILogger<DiscountService> logger, IRepository<Discount> discountRepository, ICacheService cache, IDiscountCouponFactory couponFactory, IRepository<Config> configRepository,
            IMapper mapper)
        {
            _logger = logger;
            _discountRepository = discountRepository;
            _cache = cache;
            _couponFactory = couponFactory;
            _configRepository = configRepository;
            _mapper = mapper;
        }

        public async Task<DiscountModel> GetDiscounts(int id)
        {
            DiscountModel discountModel = new();
            DateTime today = DateTime.Now;
            var discount = _discountRepository.GetFirstOrDefault(x=> x.Id == id);
            if(discount != null)
            {
                discountModel = _mapper.Map<DiscountModel>(discount);
                discountModel.discountDetail = JsonConvert.DeserializeObject<DiscountDetail>(discountModel.DetailsJson);
            }
            return await Task.FromResult(discountModel);
            
        }

        public async Task<IEnumerable<DiscountModel>> GetAllActiveDiscounts(bool includeType = true)
        {
            List<DiscountModel> discountModels = new();
            List<Discount> discounts;
            DateTime today = DateTime.Now;
            if (includeType)
            {
                discounts = _discountRepository.GetAll(x => x.IsActive == true && today >= x.CreatedDate && today <= x.ExpiryDate, new List<System.Linq.Expressions.Expression<Func<Discount, object>>> { x => x.TypeNavigation });
            }
            else
            {
                discounts = _discountRepository.GetAll(x => x.IsActive == true && today >= x.CreatedDate && today <= x.ExpiryDate);
            }

            foreach (var discount in discounts)
            {
                DiscountModel discountModel = _mapper.Map<DiscountModel>(discount);
                discountModel.discountDetail = JsonConvert.DeserializeObject<DiscountDetail>(discountModel.DetailsJson);
                discountModels.Add(discountModel);
            }

            return await Task.FromResult(discountModels);
        }

        public async Task<List<IDiscountCoupon>> GetAllDiscountCoupons()
        {
            // Idenitify if cache set date tme is set to today and no updates have been done in db
            if (ShouldUseCacheCoupons())
                return _cache.GetDiscountCoupons();

            return await BuildCouponsandStoreinCache();
        }

        public async Task ApplyDiscounts(CartModel cart, List<IDiscountCoupon> coupons)
        {
            List<DiscountResult> discountResults = new();
            foreach (var item in cart.CartItems)
            {
                DiscountResult discountResult = await ProceessDiscountforItem(cart, coupons, item);
                if (discountResult != null)
                    discountResults.Add(discountResult);
            }

            discountResults.ForEach(async x => await AppyDiscountonCartItems(cart, x));
        }

        public bool DisableDiscount(int id)
        {
            var discount = _discountRepository.GetFirstOrDefault(x => x.Id == id);
            if (discount == null)
                return false;

            discount.IsActive = false;
            _discountRepository.Update(discount);

            var config = _configRepository.GetFirstOrDefault(x => x.Key == "DiscountSetDate");
            config.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _configRepository.Update(config);

            return true;
        }

        public bool DeleteDiscount(int id)
        {
            var discount = _discountRepository.GetFirstOrDefault(x => x.Id == id);
            if (discount == null)
                return false;

            _discountRepository.Remove(discount);

            var config = _configRepository.GetFirstOrDefault(x => x.Key == "DiscountSetDate");
            config.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _configRepository.Update(config);

            return true;
        }

        public bool AddDiscount(AddDiscountModel model)
        {
            Discount discount = new Discount
            {
                Name = model.Name,
                Type = model.Type,
                IsActive = model.IsActive,
                CreatedDate = DateTime.Now,
                ExpiryDate = model.ExpiryDate,
                DetailsJson = JsonConvert.SerializeObject(model.DiscountDetail)
            };

            _discountRepository.Insert(discount);
            var config = _configRepository.GetFirstOrDefault(x => x.Key == "DiscountSetDate");
            config.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _configRepository.Update(config);

            return true;
        }

        #region private 

        private bool ShouldUseCacheCoupons()
        {
            try
            {
                var lastCachedDateTime = _cache.GetCacheKey("Discount_LastCachedDateTime");
                var discountSetDate_db = _configRepository.GetFirstOrDefault(x => x.Key == "DiscountSetDate")?.Value;

                if (discountSetDate_db == null || lastCachedDateTime == null)
                {
                    return false;
                }

                DateTime.TryParse(lastCachedDateTime, out DateTime cacheDateTime);
                DateTime.TryParse(discountSetDate_db, out DateTime dbDateTime);

                if (cacheDateTime.Date < DateTime.Now.Date || cacheDateTime < dbDateTime)
                {
                    return false;
                }
                if (_cache.GetDiscountCoupons() == null)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                // Some error occurred while identifying id can use cache. 
                // Fallback Generate coupons
                return false;
            }

        }

        private async Task<List<IDiscountCoupon>> BuildCouponsandStoreinCache()
        {
            var allactiveDiscounts = (await GetAllActiveDiscounts()).ToList();
            if (allactiveDiscounts == null)
                return null;

            var discountCoupons = _couponFactory.BuildDiscountCoupons(allactiveDiscounts);
            if (discountCoupons != null)
                _cache.SetDiscountCoupons(discountCoupons);
            return discountCoupons;
        }

        
        private async Task AppyDiscountonCartItems(CartModel cart, DiscountResult discount)
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

        private async Task<DiscountResult> ProceessDiscountforItem(CartModel cart, List<IDiscountCoupon> coupons, CartItemModel item)
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

        #endregion
    }
}
