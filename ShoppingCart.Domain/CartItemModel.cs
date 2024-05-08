namespace ShoppingCart.Domain
{
    public class CartItemModel
    {
        public string ProductCode { get; set; }
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
        public bool IsItemProcessed { get; set; }

        public decimal? Discount { get; set; }

        public string? DiscountCode { get; set; }
        public string? SpecialMessage { get; set; }

        public decimal TotalAmountBeforeDiscount { get; set; }
    }
}
