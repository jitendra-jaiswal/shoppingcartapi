using ShoppingCart.Business.Interfaces;

namespace ShoppingCart.Business
{
    public interface ICacheService
    {
        string? GetCacheKey(string key);
        bool SetCacheValue(string key, string value);
        List<IDiscountCoupon> GetDiscountCoupons();
        bool SetDiscountCoupons(List<IDiscountCoupon> coupons);
    }
    public class CacheService : ICacheService
    {
        Dictionary<string, string> keyValues;
        List<IDiscountCoupon> coupons;
        public CacheService()
        {
            keyValues = new Dictionary<string, string>();
        }

        public string? GetCacheKey(string key)
        {
            return keyValues.ContainsKey(key) ? keyValues[key] : null;
        }

        public List<IDiscountCoupon> GetDiscountCoupons()
        {
            if (coupons == null)
                return null;

            return coupons;
        }

        public bool SetCacheValue(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
                return false;

            keyValues[key] = value;
            return true;
        }

        public bool SetDiscountCoupons(List<IDiscountCoupon> coupons)
        {
            this.coupons = coupons;
            SetCacheValue("Discount_LastCachedDateTime", DateTime.Now.ToString());
            return true;
        }
    }
}
