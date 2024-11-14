using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.QuanLiTiepDon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PrivateClinic.Model;
using System.Collections.ObjectModel;
using System.Windows;
using PrivateClinic.View.MessageBox;

namespace PrivateClinic.ViewModel.QuanLiTiepDon
{
    public class SuaBenhNhanViewModel : BaseViewModel
    {
        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SetEditBNView { get; set; }
        public BENHNHAN benhnhan {  get; set; }
        //HoTen
        private string hoten;
        public string HoTen 
        { 
            get => hoten;
            set
            {
                hoten = value;
                OnPropertyChanged(nameof(HoTen));
            }
        }
        //GioiTinh
        private string gioitinh;
        public string GioiTinh
        {
            get => gioitinh;
            set
            {
                gioitinh = value;
                OnPropertyChanged(nameof(GioiTinh));
            }
        }
        //NgaySinh
        private DateTime? ngaysinh;
        public DateTime? NgaySinh
        {
            get => ngaysinh;
            set
            {
                ngaysinh = value;
                OnPropertyChanged(nameof(NgaySinh));
            }
        }

        //Dia chi
        private string diachi;
        public string DiaChi
        {
            get => diachi;
            set
            {
                diachi = value;
                OnPropertyChanged(nameof(DiaChi));
            }
        }
         private SuaBenhNhanView _view ;
        public SuaBenhNhanViewModel(SuaBenhNhanView view)
        {
            this._view = view;
            CancelCommand = new RelayCommand<SuaBenhNhanView>((p) => true, p => _CancelCommand(p));
            SaveCommand = new RelayCommand<SuaBenhNhanView>((p) => true, (p) => _SaveCommand(p));
        }

        //Load thong tin hiện tại của bệnh nhân được chọn
        public void loadEditCurrent()
        {
            HoTen = benhnhan.HoTen;
            GioiTinh = benhnhan.GioiTinh;
            DiaChi = benhnhan.DiaChi;
            NgaySinh = benhnhan.NamSinh;
        }

        //Hàm lưu thông tin đã được sửa
        private void _SaveCommand(object obj)
        {
            if (string.IsNullOrEmpty(HoTen) || string.IsNullOrEmpty(GioiTinh) || string.IsNullOrEmpty(NgaySinh.ToString()) || string.IsNullOrEmpty(DiaChi))
            {
                OkMessageBox mb = new OkMessageBox("Thông báo", "Nhập chưa đủ thông tin");
                mb.ShowDialog();
            }
            else
            {
                YesNoMessageBox mbs = new YesNoMessageBox("Thông báo", "Bạn có muốn cập nhật thông tin bệnh nhân?");
                mbs.ShowDialog();
                if (mbs.DialogResult == true )
                {
                    BENHNHAN a = DataProvider.Ins.DB.BENHNHANs.FirstOrDefault(bn => bn.MaBN == benhnhan.MaBN);
                    a.HoTen = HoTen;
                    a.GioiTinh = GioiTinh;
                    a.NamSinh = (DateTime)NgaySinh;
                    a.DiaChi = DiaChi;
                    DataProvider.Ins.DB.SaveChanges();
                    OkMessageBox mb = new OkMessageBox("Thông báo", "Thành công!");
                    mb.ShowDialog();
                    _view.Close();
                }
            }
           
        }

        void _CancelCommand(SuaBenhNhanView paramater)
        {
            paramater.Close();
        }
    }
}