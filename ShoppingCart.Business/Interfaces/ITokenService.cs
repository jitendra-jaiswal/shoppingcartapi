using ShoppingCart.Domain.Requests;
using ShoppingCart.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Business.Interfaces
{
    public interface ITokenService
    {
        User ValidateUser(LoginModel loginModel);
        string GenerateToken(User user);
    }
}
