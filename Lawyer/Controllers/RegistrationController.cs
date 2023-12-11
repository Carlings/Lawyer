using Lawyer.BLL.Interfaces;
using Lawyer.Models;
using Lawyer.Models.ViewModels;
using LawyerDataBase.DAL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Security.Claims;

namespace Lawyer.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserRoleRepository _userRoleRepository;
        public RegistrationController(IAccountService accountService, IUserRoleRepository userRoleRepository)
        {
            _accountService = accountService;
            _userRoleRepository = userRoleRepository;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            if (User.IsInRole("Адмiн"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (User.IsInRole("Lawyer"))
            {
                return RedirectToAction("Index", "Lawyer");
            }

            RegistrationUserVM registrationUserVM = new RegistrationUserVM
            {
                RoleSelectList = _userRoleRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(registrationUserVM);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationUserVM registrationUserVM)
        {
            if (ModelState.IsValid)
            {
                registrationUserVM.User.DateOfCreating = DateTime.Now;
                var result = _accountService.Register(registrationUserVM.User);

                if (result.Success)
                {
                    var authenticate_response = _accountService.Authenticate(registrationUserVM.User);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(authenticate_response));

                    return RedirectToAction("Registration");
                }
                else
                    ModelState.AddModelError(nameof(registrationUserVM.User.Name), result.Message);

                registrationUserVM.RoleSelectList = _userRoleRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                return View(registrationUserVM);
            }
            else
            {
                registrationUserVM.RoleSelectList = _userRoleRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                return View(registrationUserVM);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}