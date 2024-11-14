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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PrivateClinic.ViewModel.QuanLiTiepDon;

namespace PrivateClinic.View.QuanLiTiepDon
{
    /// <summary>
    /// Interaction logic for QuanLiKhamBenhView.xaml
    /// </summary>
    public partial class QuanLiKhamBenhView : Page
    {
        public QuanLiKhamBenhView()
        {
            InitializeComponent();
            Main.Content = new QuanLiTiepDonView();
            QuanLiKhamBenhViewModel viewModel = new QuanLiKhamBenhViewModel();
            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btnbnck.Background = Brushes.WhiteSmoke;
            btndsbn.Background = Brushes.WhiteSmoke;
            Button button = (Button)sender;
            button.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
        }
    }
}
