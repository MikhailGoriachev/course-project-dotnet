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

namespace HotelApplication.MVVM.View
{
    /// <summary>
    /// Interaction logic for RegistrationHotelWindow.xaml
    /// </summary>
    public partial class RegistrationHotelWindow : Window
    {
        public RegistrationHotelWindow()
        {
            InitializeComponent();
        }        
        
        public RegistrationHotelWindow(HistoryRegistrationHotel registrationHotel)
        {
            InitializeComponent();

            // установка DataContext
            this.DataContext = new RegistrationHotelViewModel(registrationHotel);
        }


        // закрытие приложения
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
