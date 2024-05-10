using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Responses
{
    public class LoginResponse : Response
    {
        public string Token { get; set; }
    }
}
