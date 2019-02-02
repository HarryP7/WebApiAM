using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAM.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public String Uid { get; set; } = Guid.NewGuid().ToString("D");
        [Display(Name = "Дата события")]
        public DateTime EvDate { get; set; }
        [Display(Name = "Стоимость"), Column(TypeName = "decimal(6, 2)")]
        public Decimal Cost { get; set; }
        [Display(Name = "Статус")]
        public EventStatus Status { get; set; }
        [Display(Name = "Комментарий"), Column(TypeName = "varchar(MAX)")]
        public string Comment { get; set; }
        [Display(Name = "Пользователь"), ForeignKey(nameof(User))]
        public int Fk_user { get; set; }
        public virtual User User { get; set; }
    }
}
