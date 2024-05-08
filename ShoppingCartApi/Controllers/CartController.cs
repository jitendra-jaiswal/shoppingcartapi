using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain.Models;
using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingCartApi.Controllers
{
    [Route("api/v1/[controller]/{userid}")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly IRepository<Product> _productsRepository;
        private readonly ICartService _cartService;
        public CartController(ILogger<CartController> logger, IRepository<Product> productsRepository, ICartService cartService)
        {
            _productsRepository = productsRepository; ;
            _logger = logger;
            _cartService = cartService;
        }

        // GET: api/<CartController>
        [HttpGet]
        public CartModel Get(int userid)
        {
            return _cartService.GetCart(userid);
        }

        // POST api/<CartController>
        [HttpPost]
        public ActionResult<Response> Post(int userid, [FromBody] ProductOrder order)
        {
            Response response = new() { IsSuccess = false };
            try
            {

                var product = _cartService.GetProductfromDB(order.ProductCode);
                if (product == null)
                {
                    response.ErrorMessage = "Invalid Product";
                    return BadRequest(response);
                }

                if (_cartService.AddProductToCart(product, userid, order.Quantity))
                {
                    response.IsSuccess = true;
                    return Ok(response);
                }
                response.ErrorMessage = "ErrorOccurred while saving item in cart";
                return StatusCode(500, response);
            }
            catch(Exception e)
            {
                response.ErrorMessage = e.Message;
                return StatusCode(500, response);
            }
        }

        // PUT api/<CartController>/5
        [HttpPut()]
        public ActionResult<Response> Put(int userid, [FromBody] ProductOrder order)
        {
            Response response = new() { IsSuccess = false };
            try
            {

                var product = _cartService.GetProductfromDB(order.ProductCode);
                if (product == null)
                {
                    response.ErrorMessage = "Invalid Product";
                    return BadRequest(response);
                }

                if (_cartService.UpdateProductInCart(product, userid, order.Quantity))
                {
                    response.IsSuccess = true;
                    return Ok(response);
                }
                response.ErrorMessage = "ErrorOccurred while saving item in cart";
                return StatusCode(500, response);
            }
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
                return StatusCode(500, response);
            }
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{productid}")]
        public ActionResult<Response> Delete(int userid, string productid)
        {
            Response response = new() { IsSuccess = false };
            try
            {
                if (_cartService.RemoveProductToCart(userid, productid))
                {
                    response.IsSuccess = true;
                    return Ok(response);
                }
                response.ErrorMessage = "ErrorOccurred while removing item from cart";
                return StatusCode(500, response);
            }
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
                return StatusCode(500, response);
            }
        }
    }
}
