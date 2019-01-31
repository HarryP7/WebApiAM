using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAM.Models
{
    public class User
    {
        public User() { }
        [Key]
        public int Id { get; set; }
        public String Uid { get; set; }
        [Display(Name = "Имя пользователя"), Column(TypeName = "varchar(100)")]
        public String FullName { get; set; }
        [Column(TypeName = "varchar(50)")]
        public String Email { get; set; }
        [Display(Name = "Телефон"), Column(TypeName = "varchar(16)")]
        public String Phone { get; set; }
        [Column(TypeName = "varchar(30)")]
        public String Login { get; set; }
        [ForeignKey(nameof(Role)), Display(Name = "Роль")]
        public int RoleId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [InverseProperty(nameof(Event.User))]
        public virtual ICollection<Event> Events { get; set; } = new HashSet<Event>();
        public virtual Role Role { get; set; }
    }
}