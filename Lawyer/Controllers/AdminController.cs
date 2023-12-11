using Lawyer.BLL.Helpers;
using LawyerDataBase.DAL.Entities;
using LawyerDataBase.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lawyer.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AdminController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View(_userRepository.GetAll().Where(u => u.UserRoleID == (int)RoleEnum.Lawyer));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _userRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var user = _userRepository.Find(id).Result;
            return View(user);
        }

        [HttpPost]
        public IActionResult Update(User user)
        {
            _userRepository.Update(user);
            return RedirectToAction("Index");
        }
    }
}
