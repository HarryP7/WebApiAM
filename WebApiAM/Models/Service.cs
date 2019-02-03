using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAM.Models
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
        [InverseProperty(nameof(Event.Service))]
        public virtual ICollection<Event> Events { get; set; } = new HashSet<Event>();
        public IEnumerable<Event> even;
    }
}
