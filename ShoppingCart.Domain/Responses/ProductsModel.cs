namespace ShoppingCart.Domain.Responses
{
    public class ProductsModel : Response
    {
        public List<ProductItem> Products { get; set; }
    }
}
