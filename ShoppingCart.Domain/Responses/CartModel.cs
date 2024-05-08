namespace ShoppingCart.Domain.Responses
{
    public class CartModel
    {
        public List<CartItemModel> CartItems { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalNetAmount { get; set; }

    }
}
