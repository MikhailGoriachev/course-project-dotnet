using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HotelClassLibrary.Models
{
    // Класс Событийный объект
    public class ObservableObject : INotifyPropertyChanged
    {
        // событие изменения объекта
        public event PropertyChangedEventHandler PropertyChanged;

        // изменение объекта
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
