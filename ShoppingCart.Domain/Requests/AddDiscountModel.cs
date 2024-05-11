namespace ShoppingCart.Domain.Requests
{
    public class AddDiscountModel
    {
        public string Name { get; set; } = null!;

        public int Type { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ExpiryDate { get; set; }
        public DiscountDetail DiscountDetail { get; set; }
    }
}
