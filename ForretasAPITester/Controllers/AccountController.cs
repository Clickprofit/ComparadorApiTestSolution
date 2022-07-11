using System;
using System.Security.Claims;
using ForretasAPITester.Data.Interfaces;
using ForretasAPITester.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ForretasAPITester.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly IUserRepository UserRepository;

        public AccountController(IConfiguration configuration, IUserRepository userRepository)
        {
            Configuration = configuration;
            UserRepository = userRepository;
        }

        public IActionResult Login()
        {
            LoginData loginData = new LoginData();
            return View(loginData);
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }

            LoginData loginData = new LoginData
            {
                Username = userName,
                Password = password
            };

            var merchant = UserRepository.GetUser(loginData);

            if (merchant == null)
            {
                loginData.ErrorMessage = "Invalid username or password";
                return View(loginData);
            }

            var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, merchant.Name),
                        new Claim("MerchantId", merchant.Id.ToString()),
                        new Claim("APILogin", loginData.Username),
                        new Claim("APIPassword", loginData.Password)
                    }, CookieAuthenticationDefaults.AuthenticationScheme);


            var principal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };
            _ = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
