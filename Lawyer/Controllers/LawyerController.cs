using Microsoft.AspNetCore.Http;
using Lawyer.BLL.Interfaces;
using LawyerDataBase.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LawyerDataBase.DAL.Entities;
using System.Text;
using LawyerDataBase.DAL.Repositories;

namespace Lawyer.Controllers
{
    public class LawyerController : Controller
    {
        private readonly IHTMLService _htmlService;
        private readonly IDocumentRepository _documentRepository;
        public LawyerController(IHTMLService htmlService, IDocumentRepository documentRepository)
        {
            _htmlService = htmlService;
            _documentRepository = documentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BrowseAllFiles()
        {
            var documentsResult = _documentRepository.GetAll();

            if (documentsResult.Success == true)
                return View(documentsResult.Data?.Include(o => o.User));
            else
                return Content($"{documentsResult.Message}");
        }

        [HttpPost]
        public IActionResult DeleteFile(int id)
        {
            _documentRepository.Delete(id);
            return RedirectToAction("BrowseAllFiles");
        }

        [HttpPost]
        public IActionResult Search()
        {
            string condition = Request.Form["searchField"];

            if(condition != null)
            {
                var result = _documentRepository.Search(condition);

                if (result.Success)
                    return View("BrowseAllFiles", result.Data?.Include(o => o.User));
                else
                    return Content($"{result.Message}");
            }

            return View("BrowseAllFiles");
        }

        [HttpGet]
        public IActionResult OpenFile(int id)
        {
            var result = _documentRepository.GetAll();

            if (result.Success)
            {
                var document = result.Data.FirstOrDefault(o => o.Id == id).Content;
                return Content(document, "text/html", Encoding.UTF8);
            }
            else
                return Content($"{result.Message}");
        }

        [HttpPost]
        public IActionResult DownloadFile(int id)
        {
            var result = _documentRepository.GetByID(id);
            var document = result.Data;

            if (result.Success)
            {
                byte[] fileBytes = Encoding.UTF8.GetBytes(document.Content);
                string fileName = document.Title;
                string contentType = "text/html";

                return File(fileBytes, contentType, fileName);
            }
            else
                return Content(result.Message);
        }

        public void UploadFile(IFormFile file)
        {
            string userID = User.Claims.Where(i => i.Type == "UserID").FirstOrDefault().Value;
            _htmlService.addHTMLDocument(file.OpenReadStream(), Convert.ToInt32(userID), file.FileName);
        }
    }
}
