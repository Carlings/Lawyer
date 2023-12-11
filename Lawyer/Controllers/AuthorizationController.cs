using Lawyer.BLL.Interfaces;
using Lawyer.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lawyer.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IAccountService _accountService;
        public AuthorizationController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {  
                var result = _accountService.Login(loginViewModel.Login, loginViewModel.Password);

                if(result.Success)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(result.Data));

                    // перевірка на те, який claim-role було присвоєно користувачу
                    if (result.Data.Claims.Any(claim => claim.Value == "Адмiн"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    return RedirectToAction("Index", "Lawyer");
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authorization");
        }
    }
}
