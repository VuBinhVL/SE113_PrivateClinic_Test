using PrivateClinic.Model;
using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
using System.Windows;

namespace PrivateClinic.View.QuanLiKhoThuoc
{
    /// <summary>
    /// Interaction logic for ThayDoiQuiDinhThuocView.xaml
    /// </summary>
    public partial class ThayDoiQuiDinhThuocView : Window
    {
        public ThayDoiQuiDinhThuocView()
        {
            InitializeComponent();
            SuaQuiDinhThuocViewModel viewModel =   new SuaQuiDinhThuocViewModel();
            this.DataContext = viewModel;
            //viewModel.LoadCommand.Execute(null);
            //cachdung.Text = string.Join(", ", viewModel.CachDung);
            //Tilegia.Text = string.Join(", ", viewModel.Tilegia);
            //TienKham.Text = string.Join(", ", viewModel.tienkham);
            //Dvt.Text = string.Join(", ", viewModel.dvt);
            //loaibenh.Text = string.Join(", ", viewModel.loaibenh);
            //loaithuoc.Text = string.Join(", ", viewModel.loaithuoc);
        }
    }
}
