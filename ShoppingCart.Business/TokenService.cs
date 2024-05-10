using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain.Requests;
using ShoppingCart.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoppingCart.Business
{
    public class TokenService : ITokenService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        public TokenService(IRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User> ValidateUser(LoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrWhiteSpace(loginModel.username))
                return null;

            return await Task.FromResult(_userRepository.GetFirstOrDefault(x => x.Name == loginModel.username));
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Salt"]));
            var credetial = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>(){
                    new Claim("Id",user.UserId.ToString()),
                    new Claim("Role", user.Role),
                    new Claim("Name", user.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            var token = new JwtSecurityToken(_configuration["Issuer"], _configuration["Issuer"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: credetial);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
