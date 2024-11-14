using PrivateClinic.Model;
using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PrivateClinic.View.QuanLiKhoThuoc
{
    /// <summary>
    /// Interaction logic for QuanLiKhoThuocView.xaml
    /// </summary>
    public partial class QuanLiKhoThuocView : Page
    {
        public QuanLiKhoThuocView()
        {
            InitializeComponent();
            Main.Content = new KhoThuocView();
            QuanLiKhoThuocViewModel viewModel = new QuanLiKhoThuocViewModel();
            this.DataContext = viewModel;
            viewModel.LoadCommand.Execute(null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btnKhoThuoc.Background = Brushes.WhiteSmoke;
            btnqd.Background = Brushes.WhiteSmoke;
            Button button = (Button)sender;
            button.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
        }
    }
}
