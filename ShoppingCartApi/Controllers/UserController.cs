using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain.Responses;

namespace ShoppingCartApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {
                
        }

        [HttpGet]
        public string Get(int userid)
        {
            return "Hello";
        }
    }
}
