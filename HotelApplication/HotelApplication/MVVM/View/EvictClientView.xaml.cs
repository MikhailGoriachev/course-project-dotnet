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
    /// Interaction logic for EvictClientView.xaml
    /// </summary>
    public partial class EvictClientView : Window
    {
        public EvictClientView()
        {
            InitializeComponent();
        }


        // закрытие окна
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
