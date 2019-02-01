using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiAM.Models
{
    public class AuthUserViewModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}