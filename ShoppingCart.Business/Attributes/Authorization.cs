using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoppingCart.Domain.enums;

namespace ShoppingCart.Business.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class Authorization : Attribute, IAuthorizationFilter
    {
        private readonly UserRoles _role;
        public Authorization(UserRoles role)
        {

            this._role = role; ;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var contextRole = context.HttpContext.Items["Role"];
            if (!_role.ToString().Equals(contextRole))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
