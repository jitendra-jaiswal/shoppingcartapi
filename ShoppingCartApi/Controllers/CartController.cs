using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly IRepository<Product> _productsRepository;
        public CartController(ILogger<CartController> logger, IRepository<Product> productsRepository)
        {
            _productsRepository = productsRepository; ;
            _logger = logger;
        }

        // GET: api/<CartController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productsRepository.GetAll();
        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CartController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
