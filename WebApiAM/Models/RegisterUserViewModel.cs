using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAM.Models
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Введите имя пользователя"), Display(Name = "Имя пользователя")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Введите Email")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Введите телефон"), Display(Name = "Телефон")]
        public String Phone { get; set; }
        [Required(ErrorMessage = "Введите Login")]
        public String Login { get; set; }
        public int RoleId { get; set; }

        public static explicit operator User(RegisterUserViewModel m)
        {
            return new User()
            {
                FullName = m.FullName,
                Phone = m.Phone,
                Email = m.Email,
                RoleId = m.RoleId,
                Login = m.Login
            };
        }
    }
}
