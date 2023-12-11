using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerDataBase.DAL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Вкажіть ім'я")]
        [MaxLength(30, ErrorMessage = "Ім'я повинно мати довжину не більше 30 символів")]
        [MinLength(10, ErrorMessage = "Ім'я повинно мати довжину більше 10 символів")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Вкажіть логін")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Логін не повинен мати пробілів чи спец.символів")]
        [MaxLength(20, ErrorMessage = "Логін повинен мати довжину не більше 20 символів")]
        [MinLength(5, ErrorMessage = "Логін повинен мати довжину більше 5 символів")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Вкажіть пароль")]
        [MinLength(5, ErrorMessage = "Пароль повинен мати довжину не менше 5 символів")]
        public string Password { get; set; }
        public DateTime DateOfCreating { get; set; }
        [ForeignKey("UserRole")]
        public virtual UserRole? Role { get; set; }
        public int UserRoleID { get; set; }
    }
}
