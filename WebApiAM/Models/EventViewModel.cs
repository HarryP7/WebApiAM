 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAM.Models
{
    public class EventViewModel
    {
        public String Uid { get; set; } = Guid.NewGuid().ToString("D");
        [Display(Name = "Дата события")]
        public DateTime EvDate { get; set; }
        [Display(Name = "Стоимость"), Column(TypeName = "decimal(6, 2)")]
        public Decimal Cost { get; set; }
        [Display(Name = "Комментарий"), Column(TypeName = "varchar(MAX)")]
        public string Comment { get; set; }
        [Display(Name = "Пользователь"), ForeignKey(nameof(User))]
        public int Fk_user { get; set; }
        [Display(Name = "Сервис")]
        public int Fk_service { get; set; }

        public static explicit operator Event(EventViewModel m)
        {
            return new Event()
            {
                Uid = m.Uid,
                EvDate = m.EvDate,
                Cost = m.Cost,
                Comment = m.Comment,
                Fk_user = m.Fk_user,
                Fk_service = m.Fk_service
            };
        }
    }
}
