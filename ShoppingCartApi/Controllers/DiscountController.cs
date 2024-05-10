using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        public DiscountController()
        {
            
        }
        [HttpGet]
        public string Get(int userid)
        {
            return "Hello";
        }
    }
}
