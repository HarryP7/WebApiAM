using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAM.Models
{
    public enum EventStatus
    {
        Create,//Создан
        Edit,//Редактирование
        Joined,//Присоединились
        Canceled,//Отменен
        Live,//Идет сейчас
        Completed//Завершен
    }
}
