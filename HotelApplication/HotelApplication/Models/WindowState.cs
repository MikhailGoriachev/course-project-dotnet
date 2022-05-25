using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApplication.Models
{
    // режим запуска окна
    public enum WindowState
    {
        // запуск окна в режиме создания
        CreateState,

        // запуск окна в режиме редактирования
        EditState,

        // запуск окна в режиме добавления
        AddState,

        // запуск окна в режиме удаления
        RemoveState
    }
}
