using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain.Responses;

namespace ShoppingCartApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }
        [HttpGet]
        public async Task<ActionResult<ProductsModel>> Get()
        {
            ProductsModel products = new ProductsModel() { IsSuccess = true };
            try
            {
                products.Products = await _productsService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                products.IsSuccess = false;
                products.ErrorMessage = ex.Message;
            }
            return StatusCode(500, products);
        }
    }
}
