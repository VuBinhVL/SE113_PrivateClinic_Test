using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
using System.Windows;

namespace PrivateClinic.View.QuanLiKhoThuoc
{
    /// <summary>
    /// Interaction logic for NhapThuocView.xaml
    /// </summary>
    public partial class NhapThuocView : Window
    {
        public NhapThuocView()
        {
            InitializeComponent();
            Main.Content = new ThemThuocCuView();
            ThuocCuRBtn.IsChecked = true;
            NhapThuocViewModel viewModel = new NhapThuocViewModel();
            this.DataContext = viewModel;
        }
      
    }
}
