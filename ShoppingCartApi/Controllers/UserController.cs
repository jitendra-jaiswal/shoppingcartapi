using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain.Requests;
using ShoppingCart.Domain.Responses;

namespace ShoppingCartApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public UserController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public ActionResult<LoginResponse> Login(LoginModel loginRequest)
        {
            LoginResponse response = new() { IsSuccess = false };
            try
            {
                var user = _tokenService.ValidateUser(loginRequest);
                if (user == null)
                {
                    response.ErrorMessage = "Invadlid Credentials";
                    return BadRequest(response);
                }

                response.Token = _tokenService.GenerateToken(user);
                response.IsSuccess = true;
                return Ok(response);
            }catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return StatusCode(500, response);
        }
    }
}
