using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business;
using ShoppingCart.Business.Attributes;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain.enums;
using ShoppingCart.Domain.Responses;
using Newtonsoft.Json;
using ShoppingCart.Domain;
using ShoppingCart.Domain.Requests;

namespace ShoppingCartApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorization(UserRoles.Admin)]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<ActionResult<DiscountsResponse>> Get()
        {
            DiscountsResponse response = new() { IsSuccess = false };
            try
            {
                response.discounts = (await _discountService.GetAllActiveDiscounts(false)).ToList();
                var result = JsonConvert.SerializeObject(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }
            return StatusCode(500, response);
        }

        [HttpGet]
        [Route("/{discountid}")]
        public async Task<ActionResult<DiscountsResponse>> Get(int discountid)
        {
            DiscountsResponse response = new() { IsSuccess = false };
            try
            {
                response.discounts.Add(_discountService.GetDiscounts(discountid).GetAwaiter().GetResult());
                var result = JsonConvert.SerializeObject(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }
            return StatusCode(500, response);

        }

        [HttpDelete]
        [Route("/{discountid}/disable")]
        public async Task<ActionResult<Response>> Disable(int discountid)
        {
            Response response = new() { IsSuccess = false };
            try
            {
                _discountService.DisableDiscount(discountid);
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }
            return StatusCode(500, response);

        }

        [HttpDelete]
        [Route("/{discountid}")]
        public async Task<ActionResult<Response>> Delete(int discountid)
        {
            Response response = new() { IsSuccess = false };
            try
            {
                _discountService.DeleteDiscount(discountid);
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }
            return StatusCode(500, response);

        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post(AddDiscountModel discountModel)
        {
            Response response = new() { IsSuccess = false };
            try
            {
                _discountService.AddDiscount(discountModel);
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }
            return StatusCode(500, response);

        }
    }
}
