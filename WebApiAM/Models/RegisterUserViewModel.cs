using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAM.Models
{
    public class RegisterUserViewModel
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
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
