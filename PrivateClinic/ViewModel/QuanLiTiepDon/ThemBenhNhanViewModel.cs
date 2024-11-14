using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.QuanLiTiepDon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using PrivateClinic.Model;
using System.Collections.ObjectModel;
using PrivateClinic.View.MessageBox;

namespace PrivateClinic.ViewModel.QuanLiTiepDon
{
    public class ThemBenhNhanViewModel : BaseViewModel
    {
        //Lấy danh sách bệnh nhân 
        private ObservableCollection<BENHNHAN> listBenhNhan; 
        public ObservableCollection<BENHNHAN> ListBenhNhan
        {
            get => listBenhNhan;
            set
            {
                listBenhNhan = value;
                OnPropertyChanged(nameof(ListBenhNhan));
            }
        }

        private BENHNHAN selectedBenhNhan;
        public BENHNHAN SelectedBenhNhan
        {
            get => selectedBenhNhan;
            set
            {
                if (selectedBenhNhan != value)
                {
                    selectedBenhNhan = value;
                    OnPropertyChanged(nameof(SelectedBenhNhan));
                    XacDinhThongTinChoBenhNhanCu();
                }
                
            }
        }

        //Địa chỉ
        private string diachi;
        public string DiaChi
        {
            get => diachi;
            set
            {
                diachi = value;
                OnPropertyChanged(nameof (DiaChi));
            }
        }
        //Giới tính
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
        //Ngày sinh
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

        public ICommand AddBN {  get; set; }
        public ICommand CancelCommand {  get; set; }
        public ThemBenhNhanViewModel()
        {
            ListBenhNhan = new ObservableCollection<BENHNHAN>(DataProvider.Ins.DB.BENHNHANs);
            AddBN = new RelayCommand<ThemBenhNhanView>((p) => true, (p) => _AddBN(p));
            CancelCommand = new RelayCommand<ThemBenhNhanView>((p) => true, (p) => _CancelCommand(p));
        }
        
        void _AddBN(ThemBenhNhanView paramater)
        {
            QuanLiTiepDonViewModel model = new QuanLiTiepDonViewModel();
            THAMSO t = DataProvider.Ins.DB.THAMSOes.SingleOrDefault(h => h.MaThamSo == 1);
            if (string.IsNullOrEmpty(paramater.HoTen.Text) || string.IsNullOrEmpty(paramater.GioiTinh.SelectedItem.ToString()) || string.IsNullOrEmpty(paramater.NgSinh.Text) || string.IsNullOrEmpty(paramater.DiaChi.Text))
            {
                OkMessageBox mb = new OkMessageBox("Thông báo", "Chưa nhập đủ thông tin");
                mb.ShowDialog();
            }
            else if (model.SoLuongBenhNhanDaTiepDon == t.GiaTri)
            {
                OkMessageBox mb = new OkMessageBox("Thông báo", "Đã đạt tối đa bệnh nhân tiếp đón trong ngày");
                mb.ShowDialog();
            }
            else
            {
                var benhnhan = ListBenhNhan.FirstOrDefault(bn => bn.MaBN == SelectedBenhNhan?.MaBN);
                if (benhnhan != null)
                {
                    YesNoMessageBox h = new YesNoMessageBox("Thông báo", "Bạn có muốn thêm bệnh nhân này vào danh sách tiếp đón");
                    h.ShowDialog();
                    if (h.DialogResult == true)
                    {
                        benhnhan.TrangThai = false;
                        benhnhan.DiaChi = paramater.DiaChi.Text;
                        benhnhan.GioiTinh = paramater.GioiTinh.Text;
                        benhnhan.NamSinh = (DateTime)paramater.NgSinh.SelectedDate;
                        benhnhan.HoTen = paramater.HoTen.Text;
                        DataProvider.Ins.DB.SaveChanges();
                        OkMessageBox mb = new OkMessageBox("Thông báo", "Thành công");
                        mb.ShowDialog();
                    }
                        
                }
                else
                {
                    YesNoMessageBox h = new YesNoMessageBox("Thông báo", "Bạn có muốn thêm bệnh nhân này vào danh sách tiếp đón");
                    h.ShowDialog();
                    if (h.DialogResult == true)
                    {
                        BENHNHAN a = new BENHNHAN();
                        a.HoTen = paramater.HoTen.Text;
                        a.GioiTinh = paramater.GioiTinh.Text;
                        a.DiaChi = paramater.DiaChi.Text;
                        a.NamSinh = (DateTime)paramater.NgSinh.SelectedDate;
                        a.TrangThai = false;
                        OkMessageBox mb = new OkMessageBox("Thông báo", "Thành công");
                        mb.ShowDialog();
                        DataProvider.Ins.DB.BENHNHANs.Add(a);
                        DataProvider.Ins.DB.SaveChanges();
                        
                    }
                }
                // Xóa thông tin các TextBox
                paramater.HoTen.Text = string.Empty;
                paramater.GioiTinh.SelectedIndex = -1;
                paramater.DiaChi.Clear();
                paramater.NgSinh.SelectedDate = null;
            }
        }
        void _CancelCommand(ThemBenhNhanView paramater)
        {       
            paramater.Close();
        }

        private void XacDinhThongTinChoBenhNhanCu()
        {
            if (SelectedBenhNhan != null)
            {
                var benhnhan = ListBenhNhan.FirstOrDefault(bn => bn.MaBN == SelectedBenhNhan.MaBN);
                if (benhnhan != null) 
                {
                    DiaChi = benhnhan.DiaChi;
                    NgaySinh = benhnhan.NamSinh;
                    GioiTinh = benhnhan.GioiTinh;
                }
                else
                {
                    DiaChi = "";
                    NgaySinh = null;
                    GioiTinh = "";
                }
            }
            else
            {
                DiaChi = "";
                NgaySinh = null;
                GioiTinh = "";
            }
        }
    }
}
