namespace ShoppingCart.Domain
{
    public class DiscountResult
    {
        public string ProductCode { get; set; }
        public string DiscountCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public string OnProductCode { get; set; }
        public string SpecialMessage { get; set; }
    }
}
