using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Attributes;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain.enums;
using ShoppingCart.Domain.Models;
using ShoppingCart.Domain.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingCartApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorization(UserRoles.User)]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly IDiscountService _discountService;
        private readonly ICartService _cartService;
        public CartController(ILogger<CartController> logger, IDiscountService discountService, ICartService cartService)
        {
            _discountService = discountService;
            _logger = logger;
            _cartService = cartService;
        }

        // GET: api/<CartController>
        [HttpGet]
        public async Task<ActionResult<CartModel>> Get()
        {
            CartModel cart = new CartModel() { IsSuccess = true };
            try
            {
                _logger.LogInformation("Initiating Get Cart");
                Int32.TryParse(this.HttpContext.Items["UserId"]?.ToString(), out int userid);
                cart = await _cartService.GetCart(userid);
                if (cart.CartItems.Any())
                {
                    var discounts = await _discountService.GetAllDiscountCoupons();
                    if (discounts.Any())
                    {
                        await _discountService.ApplyDiscounts(cart, discounts);
                    }
                }
                cart.TotalAmount = cart.CartItems.Sum(x => x.TotalAmountBeforeDiscount);
                cart.TotalDiscount = cart.CartItems.Sum(x => x.Discount ?? 0);
                cart.TotalNetAmount = cart.TotalAmount - cart.TotalDiscount;
                _logger.LogInformation("Get Cart request completed successfully");
                return Ok(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Get Cart. Message: {ex.Message} StackTrace: {ex.StackTrace}");
                cart.IsSuccess = false;
                cart.ErrorMessage = ex.Message;
            }
            return StatusCode(500, cart);
        }

        // POST api/<CartController>
        [HttpPost]
        public async Task<ActionResult<Response>> Post([FromBody] ProductOrder order)
        {
            Response response = new() { IsSuccess = false };
            try
            {
                _logger.LogInformation("Initialized Delete Cart request");
                Int32.TryParse(this.HttpContext.Items["UserId"]?.ToString(), out int userid);
                var product = await _cartService.GetProductfromDB(order.ProductCode);
                if (product == null)
                {
                    response.ErrorMessage = "Invalid Product";
                    return BadRequest(response);
                }

                if (await _cartService.UpdateProductInCart(product, userid, order.Quantity))
                {
                    response.IsSuccess = true;
                    _logger.LogInformation("Post Cart request completed successfully");
                    return Ok(response);
                }
                response.ErrorMessage = "ErrorOccurred while saving item in cart";
                return StatusCode(500, response);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occurred in Post Cart. Message: {e.Message} StackTrace: {e.StackTrace}");
                response.ErrorMessage = e.Message;
                return StatusCode(500, response);
            }
        }

        // PUT api/<CartController>/5
        [HttpPut()]
        public async Task<ActionResult<Response>> Put([FromBody] ProductOrder order)
        {
            Response response = new() { IsSuccess = false };
            try
            {
                _logger.LogInformation("Initialized Delete Cart request");
                Int32.TryParse(this.HttpContext.Items["UserId"]?.ToString(), out int userid);
                var product = await _cartService.GetProductfromDB(order.ProductCode);
                if (product == null)
                {
                    response.ErrorMessage = "Invalid Product";
                    return BadRequest(response);
                }

                if (await _cartService.UpdateProductInCart(product, userid, order.Quantity))
                {
                    _logger.LogInformation("Put Cart request completed successfully");
                    response.IsSuccess = true;
                    return Ok(response);
                }
                response.ErrorMessage = "ErrorOccurred while saving item in cart";
                return StatusCode(500, response);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occurred in Put Cart. Message: {e.Message} StackTrace: {e.StackTrace}");
                response.ErrorMessage = e.Message;
                return StatusCode(500, response);
            }
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{productid}")]
        public async Task<ActionResult<Response>> Delete(string productid)
        {
            Response response = new() { IsSuccess = false };
            try
            {
                _logger.LogInformation("Initialized Delete Cart request");
                Int32.TryParse(this.HttpContext.Items["UserId"]?.ToString(), out int userid);
                if (await _cartService.RemoveProductToCart(userid, productid))
                {
                    _logger.LogInformation("Delete Cart request completed successfully");
                    response.IsSuccess = true;
                    return Ok(response);
                }
                response.ErrorMessage = "ErrorOccurred while removing item from cart";
                return StatusCode(500, response);
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception occurred in Delete Cart. Message: {e.Message} StackTrace: {e.StackTrace}");
                response.ErrorMessage = e.Message;
                return StatusCode(500, response);
            }
        }
    }
}
