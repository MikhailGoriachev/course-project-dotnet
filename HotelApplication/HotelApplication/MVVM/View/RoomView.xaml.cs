using HotelApplication.MVVM.ViewModel;
using HotelClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using HotelApplication.Models;

namespace HotelApplication.MVVM.View
{
    /// <summary>
    /// Interaction logic for RoomView.xaml
    /// </summary>
    public partial class RoomView : Window
    {
        public RoomView()
        {
            InitializeComponent();
        }


        public RoomView(HotelRoom room)
        {
            InitializeComponent();

            this.DataContext = new RoomViewModel(room);
        }


        // закрытие окна
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
