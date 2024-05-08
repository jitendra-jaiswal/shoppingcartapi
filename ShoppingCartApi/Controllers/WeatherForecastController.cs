using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Interfaces;

namespace ShoppingCartApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        IDiscountService _discountService;
        public WeatherForecastController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public void Get()
        {
            var discounts = _discountService.GetAllActiveDiscounts();
        }
    }
}
