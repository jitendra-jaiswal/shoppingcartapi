namespace ShoppingCart.Domain.Responses
{
    public class DiscountsResponse : Response
    {
        public DiscountsResponse()
        {
            discounts = new();
        }
        public List<DiscountModel> discounts { get; set; }
    }
}
