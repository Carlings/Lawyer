using System.ComponentModel.DataAnnotations;

namespace Lawyer.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Вкажіть логін")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Логін не повинен мати пробілів чи спец.символів")]
        [MaxLength(20, ErrorMessage = "Логін повинен мати довжину не більше 20 символів")]
        [MinLength(5, ErrorMessage = "Логін повинен мати довжину більше 5 символів")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Вкажіть пароль")]
        [MinLength(5, ErrorMessage = "Пароль повинен мати довжину не менше 5 символів")]
        public string Password { get; set; }
    }
}
