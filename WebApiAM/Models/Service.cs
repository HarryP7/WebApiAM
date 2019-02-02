using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvAgency.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Широта"), Column(TypeName = "decimal(10, 6)")]
        public Decimal Lat { get; set; }
        [Display(Name = "Долгота"), Column(TypeName = "decimal(10, 6)")]
        public Decimal Lng { get; set; }
        [Display(Name = "Название"), Column(TypeName = "varchar(100)")]
        public String Title { get; set; }
        [Display(Name = "Дата размещения")]
        public DateTime DatePlace { get; set; }
        public string ImageUrl { get; set; }
        //  [Display(Name = "Целевая группа"), Column(TypeName = "varchar(200)")]
        //  public String TargetGroup { get; set; }
        //  [Display(Name = "Описание"), Column(TypeName = "varchar(MAX)")]
        //  public String Description { get; set; }
        //  [Display(Name = "Отзыв"), Column(TypeName = "varchar(MAX)")]
        //  public String Review { get; set; }
        //  [Display(Name = "Рейтинг"), Column(TypeName = "varchar(MAX)")]
        //  public int Rating { get; set; } //1-5
    }
}
