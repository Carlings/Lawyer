using LawyerDataBase.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Lawyer.Models.ViewModels
{
    public class RegistrationUserVM
    {
        public User User { get; set; }
        public IEnumerable<SelectListItem>? RoleSelectList { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Приміть політику приватності")]
        public bool AcceptTerms { get; set; }
    }
}
