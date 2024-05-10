using ShoppingCart.Domain.Requests;
using ShoppingCart.Infrastructure;

namespace ShoppingCart.Business.Interfaces
{
    public interface ITokenService
    {
        Task<User> ValidateUser(LoginModel loginModel);
        string GenerateToken(User user);
    }
}
