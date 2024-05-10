using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ShoppingCart.Business.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public AuthMiddleware(RequestDelegate _next, IConfiguration configuration)
        {
            this._next = _next;
            _configuration = configuration;
        }
        public async Task Invoke(HttpContext context)
        {

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                //Validate Token
                bool isTokenValid = ParseToken(context, token);
                if (!isTokenValid)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }
            _ = _next(context);
        }

        private bool ParseToken(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Salt"]));
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = _configuration["Issuer"],
                    ValidAudience = _configuration["Issuer"]
                }, out SecurityToken validateToken);


                var jwtToken = (JwtSecurityToken)validateToken;
                context.Items["UserId"] = int.Parse(jwtToken.Claims.FirstOrDefault(_ => _.Type == "Id").Value);
                context.Items["Name"] = jwtToken.Claims.FirstOrDefault(_ => _.Type == "Name").Value;
                context.Items["Role"] = jwtToken.Claims.FirstOrDefault(_ => _.Type == "Role").Value;
                return true;
            }
            catch (Exception ex)
            {
                // Log Token Validation failed. Unable to Validate Token
                return false;

            }
        }
    }
}
