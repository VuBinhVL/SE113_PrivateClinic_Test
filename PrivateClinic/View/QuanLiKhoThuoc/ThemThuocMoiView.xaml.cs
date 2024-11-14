using PrivateClinic.Model;
using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
using System;
using System.Linq;
using System.Windows.Controls;

namespace PrivateClinic.View.QuanLiKhoThuoc
{
    /// <summary>
    /// Interaction logic for ThemThuocMoiView.xaml
    /// </summary>
    public partial class ThemThuocMoiView : UserControl
    {
        public ThemThuocMoiView()
        {
            InitializeComponent();
            ThemThuocMoiViewModel viewModel = new ThemThuocMoiViewModel();
            this.DataContext = viewModel;
            viewModel.LoadCommand.Execute(null);
            TenDVTcbx.ItemsSource = viewModel.TenDVTs;
            CachDungcbx.ItemsSource = viewModel.CachDung;
            LoaiThuoccbx.ItemsSource = viewModel.LoaiThuoc;
            NgayNhap.SelectedDate = DateTime.Now;
            NgayNhap.Text = DateTime.Now.ToString();
            int nextMathuoc;
            nextMathuoc = DataProvider.Ins.DB.THUOCs.Max(x=> x.MaThuoc) + 1;
            THUOC thuocmoiload = new THUOC();
            thuocmoiload.MaThuoc = nextMathuoc;
            MaThuoc.Text = "Med" +thuocmoiload.MaThuoc.ToString();
            
            


        }
    }
}
