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
        [Display(Name = "Широта"), Column(TypeName = "decimal(10, 6)")]
        public Decimal Lat { get; set; }
        [Display(Name = "Долгота"), Column(TypeName = "decimal(10, 6)")]
        public Decimal Lng { get; set; }
        [Display(Name = "Название"), Column(TypeName = "varchar(100)")]
        public String Title { get; set; }
        [Display(Name = "Дата размещения")]
        public DateTime DatePlace { get; set; }
        [Display(Name = "Дата события")]
        public DateTime EvDate { get; set; }
        [Display(Name = "Стоимость"), Column(TypeName = "decimal(6, 2)")]
        public Decimal Cost { get; set; }
        [ForeignKey(nameof(User)), Display(Name = "Пользователь")]
        public int Fk_user { get; set; }

        public virtual User User { get; set; }
    }
}
