using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain;
using ShoppingCart.Infrastructure;

namespace ShoppingCart.Business
{

    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> _productRepository;
        public ProductsService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductItem>> GetAllProducts()
        {
            var products = _productRepository.GetAll(includes: new List<System.Linq.Expressions.Expression<Func<Product, object>>> { x => x.CategoryNavigation });

            var productsList = products.Select(x => new ProductItem
            {
                Name = x.Name,
                category = x.CategoryNavigation?.CategoryName,
                Description = x.Properties,
                ProductCode = x.ProductCode,
                UnitPrice = x.UnitPrice
            });
            return await Task.FromResult(productsList.ToList());
        }
    }
}
