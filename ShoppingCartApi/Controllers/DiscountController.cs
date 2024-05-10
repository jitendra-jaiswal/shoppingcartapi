using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Attributes;
using ShoppingCart.Domain.enums;

namespace ShoppingCartApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorization(UserRoles.Admin)]
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
