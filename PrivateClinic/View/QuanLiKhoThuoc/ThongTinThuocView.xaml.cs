using System.Windows;
using PrivateClinic.ViewModel.QuanLiKhoThuocVM;

namespace PrivateClinic.View.QuanLiKhoThuoc
{
    /// <summary>
    /// Interaction logic for ThongTinThuocView.xaml
    /// </summary>
    public partial class ThongTinThuocView : Window
    {
        public ThongTinThuocView()
        {
            InitializeComponent();
            ThongTinThuocViewModel viewModel= new ThongTinThuocViewModel(); 
            this.DataContext = viewModel;
        }
    }
}
