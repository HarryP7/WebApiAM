using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiAM.Models
{
    public class AuthUserViewModel
    {
        [Required(ErrorMessage = "�� ������ Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "�� ������ ������")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}