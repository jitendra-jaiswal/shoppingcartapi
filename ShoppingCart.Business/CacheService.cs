using ShoppingCart.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Business
{
    public interface ICacheService
    {
        string? GetCacheKey(string key);
        bool SetCacheValue(string key, string value);
        List<IDiscountCoupon> GetDiscountCoupons();
        bool SetDiscountCoupons(List<IDiscountCoupon> coupons);
    }
    public class CacheService: ICacheService
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
            return coupons;
        }

        public bool SetCacheValue(string key, string value)
        {
            keyValues[key] = value;
            return true;
        }

        public bool SetDiscountCoupons(List<IDiscountCoupon> coupons)
        {
            this.coupons = coupons;
            return true;
        }
    }
}
