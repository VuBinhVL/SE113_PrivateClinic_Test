
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.ViewModel.QuanLiKhoThuocVM;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class SuaNhomNguoiDungViewModel : BaseViewModel
    {
        public static SuaNhomNguoiDungViewModel Instance { get; set; } = new SuaNhomNguoiDungViewModel();

        public SuaNhomNguoiDungView EditNhomNDView { get; set; }
        private ObservableCollection<NHOMNGUOIDUNG> _nhomND;

        public ObservableCollection<NHOMNGUOIDUNG> nhomND
        {
            get => _nhomND;
            set { _nhomND = value; OnPropertyChanged(nameof(nhomND)); }

        }
        public ICommand SaveCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public SuaNhomNguoiDungViewModel()
        {
            ExitCommand = new RelayCommand<SuaNhomNguoiDungView>((p) => true, (p) => Close(p));
            SaveCommand = new RelayCommand<SuaNhomNguoiDungView>((p) => true, (p) => _saveCommand(p));
        }
        void Close(SuaNhomNguoiDungView p)
        {
            p.Close();
        }
        private void _saveCommand(SuaNhomNguoiDungView p)
        {
            nhomND = new ObservableCollection<NHOMNGUOIDUNG>(DataProvider.Ins.DB.NHOMNGUOIDUNGs);
            var pp = new SuaNhomNguoiDungView();
            pp.TenChucNang.Text = SuaNhomNguoiDungViewModel.Instance.EditNhomNDView.TenChucNang.Text;
            pp.NhomNDcbx.Text = SuaNhomNguoiDungViewModel.Instance.EditNhomNDView.NhomNDcbx.Text;

            MessageBoxResult result = MessageBox.Show("Bạn muốn thay đổi nhóm người dùng của chức năng này?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {


                CHUCNANG cn = DataProvider.Ins.DB.CHUCNANGs.FirstOrDefault(c => c.TenChucNang == pp.TenChucNang.Text);
                PHANQUYEN phanquyen = DataProvider.Ins.DB.PHANQUYENs.FirstOrDefault(t => t.MaChucNang == cn.MaChucNang);

                if (phanquyen != null)
                {
                    NHOMNGUOIDUNG nd = DataProvider.Ins.DB.NHOMNGUOIDUNGs.FirstOrDefault(n => n.TenNhom == pp.NhomNDcbx.Text);
                    phanquyen.MaNhom = nd.MaNhom;
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật nhóm người dùng của chức năng này thành công!", "THÔNG BÁO");


                    PhanQuyenUS phanQuyenView = new PhanQuyenUS();
                    phanQuyenView.ListViewPQ.ItemsSource = new ObservableCollection<PQDTO>();
                    phanQuyenView.ListViewPQ.Items.Refresh();
                    p.Close();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy chức năng này.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
