using PrivateClinic.Model;
using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
using System.Collections.ObjectModel;
using System.Windows;

namespace PrivateClinic.View.QuanLiKhoThuoc
{
    /// <summary>
    /// Interaction logic for ThayDoiThongTinThuocView.xaml
    /// </summary>
    public partial class ThayDoiThongTinThuocView : Window
    {
        public ThayDoiThongTinThuocView()
        {
            InitializeComponent();
            SuaThongTinThuocViewModel viewModel = new SuaThongTinThuocViewModel();
            this.DataContext = viewModel;
            viewModel.LoadCommand.Execute(null);
            TenDVTcbx.ItemsSource = viewModel.TenDVTs;
        }
        private void TenDVTcbx_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is SuaThongTinThuocViewModel viewModel)
            {
                
                    viewModel.LoadCommand.Execute(null);
                //TenDVTcbx.Items.Refresh();
                TenDVTcbx.ItemsSource = viewModel.TenDVTs;

            }
        }
    }
}
