using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Attributes;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain.enums;
using ShoppingCart.Domain.Models;
using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;

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
        public CartModel Get()
        {
            Int32.TryParse(this.HttpContext.Items["UserId"]?.ToString(), out int userid);
            var cart = _cartService.GetCart(userid);
            if (cart.CartItems.Any())
            {
                var discounts = _discountService.GetAllDiscountCoupons();
                if(discounts.Any())
                {
                    _discountService.ApplyDiscounts(cart, discounts);
                }
            }
            cart.TotalAmount = cart.CartItems.Sum(x => x.TotalAmountBeforeDiscount);
            cart.TotalDiscount = cart.CartItems.Sum(x => x.Discount ?? 0);
            cart.TotalNetAmount = cart.TotalAmount - cart.TotalDiscount;
            return cart;
        }

        // POST api/<CartController>
        [HttpPost]
        public ActionResult<Response> Post([FromBody] ProductOrder order)
        {
            Response response = new() { IsSuccess = false };
            try
            {
                Int32.TryParse(this.HttpContext.Items["UserId"]?.ToString(), out int userid);
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
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
                return StatusCode(500, response);
            }
        }

        // PUT api/<CartController>/5
        [HttpPut()]
        public ActionResult<Response> Put([FromBody] ProductOrder order)
        {
            Response response = new() { IsSuccess = false };
            try
            {
                Int32.TryParse(this.HttpContext.Items["UserId"]?.ToString(), out int userid);
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
        public ActionResult<Response> Delete(string productid)
        {
            Response response = new() { IsSuccess = false };
            try
            {
                Int32.TryParse(this.HttpContext.Items["UserId"]?.ToString(), out int userid);
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
