using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.BLL.Interfaces;
using ToDo.BLL.Models.Response;
using ToDo.BLL.Models.Request;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace ToDo.Controllers
{
    public class UserController : Controller
    {
        private readonly IRegistrationService _registrationService;
        private readonly ILoginService _loginService;

        public UserController(IRegistrationService registrationService, ILoginService loginService)
        {
            _loginService = loginService;
            _registrationService = registrationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegistrationRequest userModel)
        {
            var result = await _registrationService.Registration(userModel);
            return RedirectToAction(nameof(UserController.RegisterComplete), result);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterComplete(RegistrationResponse registerResponse)
        {
            return View(registerResponse);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var claims = await _loginService.Login(loginRequest);

            var identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimTypes.NameIdentifier, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(UserController.Unauthorized), "User");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Unauthorized()
        {
            return View();
        }

    }

}