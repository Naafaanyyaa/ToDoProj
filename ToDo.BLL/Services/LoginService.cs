using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ToDo.BLL.Infrastructure;
using ToDo.BLL.Interfaces;
using ToDo.BLL.Models.Request;
using ToDo.BLL.Models.Response;
using ToDo.DAL.Entities.Identity;

namespace ToDo.BLL.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<UserDto> _userManager;

        private readonly JwtHandler _jwtHandler;

        public LoginService(UserManager<UserDto> userManager, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        public async Task<List<Claim>> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return new List<Claim>();
            }

            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            return claims;
        }
    }
}
