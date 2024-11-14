using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.View.MessageBox;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class SuaThongTinBacSiViewModel : BaseViewModel
    {
        #region Các command và Property
        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public BACSI bacsi { get; set; }
        //Họ Tên
        private string hoten;
        public string HoTen
        {
            get => hoten;
            set
            {
                hoten = value;
                ValidateFullName();
            }
        }

        private string hotenError;
        public string HoTenError
        {
            get => hotenError;
            set
            {
                hotenError = value;
                OnPropertyChanged(nameof(HoTenError));
            }
        }

        //Email
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
            }
        }

        private string emailError;
        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged(nameof(EmailError));
            }
        }

        //SĐT
        private string sdt;
        public string SDT
        {
            get => sdt;
            set
            {
                sdt = value;
                ValidateSDT();
            }
        }

        private string sdtError;
        public string SDTError
        {
            get => sdtError;
            set
            {
                sdtError = value;
                OnPropertyChanged(nameof(SDTError));
            }
        }

        //DiaChi
        private string diachi;
        public string DiaChi
        {
            get => diachi;
            set
            {
                diachi = value;
                ValidateDiaChi();
            }
        }

        private string diachiError;
        public string DiaChiError
        {
            get => diachiError;
            set
            {
                diachiError = value;
                OnPropertyChanged(nameof(DiaChiError));
            }
        }

        //BangCap
        private string bangcap;
        public string BangCap
        {
            get => bangcap;
            set
            {
                bangcap = value;
                ValidateBangCap();
            }
        }

        private string bangcapError;
        public string BangCapError
        {
            get => bangcapError;
            set
            {
                bangcapError = value;
                OnPropertyChanged(nameof(BangCapError));
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
                ValidateBirth();
            }
        }
        private string ngaysinhError;
        public string NgaySinhError
        {
            get => ngaysinhError;
            set
            {
                ngaysinhError = value;
                OnPropertyChanged(nameof(NgaySinhError));
            }
        }
        //NgayVaoLam
        private DateTime? ngayvl;
        public DateTime? NgayVL
        {
            get => ngayvl;
            set
            {
                ngayvl = value;
                ValidateNgayVL();
            }
        }
        private string ngayvlError;
        public string NgayVLError
        {
            get => ngayvlError;
            set
            {
                ngayvlError = value;
                OnPropertyChanged(nameof(NgayVLError));
            }
        }
#endregion
        private SuaThongTinBacSiView _view;
        private bool[] _canAccept = new bool[7];

        //Hàm khởi tạo
        public SuaThongTinBacSiViewModel(SuaThongTinBacSiView view)
        {
            this._view = view;
            CancelCommand = new ViewModelCommand(quit);
            SaveCommand = new ViewModelCommand(accept, canAccept);

        }
               
        //Hàm cho phép nút sửa có hiển thị không
        private bool canAccept(object obj)
        {
            bool check = _canAccept[0] && _canAccept[1] && _canAccept[2] && _canAccept[3]
                            && _canAccept[4] && _canAccept[5] && _canAccept[6];
            if (check)
            {
                return true;
            }
            return false;

        }

        //Hàm sửa thông tin bác sĩ
        private void accept(object obj)
        {
            YesNoMessageBox h = new YesNoMessageBox("Thông báo", "Bạn muốn lưu thông tin bác sĩ ?");
            if (h.DialogResult == true)
            {
                BACSI a = DataProvider.Ins.DB.BACSIs.FirstOrDefault(bs => bs.MaBS == bacsi.MaBS);
                a.HoTen = HoTen;
                a.SDT = SDT;
                a.GioiTinh = GioiTinh;
                a.Email = Email;
                a.NgaySinh = NgaySinh;
                a.NgayVaoLam =(DateTime)NgayVL;
                a.DiaChi = DiaChi;
                a.BangCap = BangCap;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Thành công", "Thông báo");
            }
        }


        //Hàm load thông tin hiện tại của item đang được chọn
        public void loadEditCurrent()
        {
            HoTen = bacsi.HoTen;
            Email = bacsi.Email;
            GioiTinh = bacsi.GioiTinh;
            SDT = bacsi.SDT;
            DiaChi = bacsi.DiaChi;
            BangCap = bacsi.BangCap;
            NgaySinh = bacsi.NgaySinh;
            NgayVL = bacsi.NgayVaoLam;
        }
        
        //Hàm hỗ trợ CancelCommand
        private void quit(object obj)
        {
            _view.Close();
        }

        #region Các hàm báo lỗi
        private void ValidateFullName()
        {
            if (string.IsNullOrWhiteSpace(HoTen))
            {
                HoTenError = "Họ và tên không được để trống!";
                _canAccept[0] = false;
            }
            else
            {
                HoTenError = "";
                _canAccept[0] = true;
            }
        }
        private void ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailError = "Email không được để trống!";
                _canAccept[1] = false;
            }
            else if (!Email.Contains("@"))
            {
                EmailError = "Email không hợp lệ!";
                _canAccept[1] = false;
            }
            else
            {
                EmailError = "";
                _canAccept[1] = true;
            }
        }
        private void ValidateSDT()
        {
            if (string.IsNullOrWhiteSpace(SDT))
            {
                SDTError = "SĐT không được để trống!";
                _canAccept[2] = false;
            }
            else if (!SDT.All(char.IsDigit))
            {
                SDTError = "SĐT chỉ chứa số";
                _canAccept[2] = false;
            }
            else
            {
                SDTError = "";
                _canAccept[2] = true;
            }
        }
        private void ValidateDiaChi()
        {
            if (string.IsNullOrWhiteSpace(DiaChi))
            {
                DiaChiError = "Địa chỉ không được để trống!";
                _canAccept[3] = false;
            }
            else
            {
                DiaChiError = "";
                _canAccept[3] = true;
            }
        }
        private void ValidateBangCap()
        {
            if (string.IsNullOrWhiteSpace(BangCap))
            {
                BangCapError = "Bằng cấp không được để trống!";
                _canAccept[4] = false;
            }
            else
            {
                BangCapError = "";
                _canAccept[4] = true;
            }
        }
        private void ValidateBirth()
        {
            if (NgaySinh > DateTime.UtcNow)
            {
                NgaySinhError = "Ngày sinh không hợp lệ!";
                _canAccept[5] = false;
            }
            else
            {
                NgaySinhError = "";
                _canAccept[5] = true;
            }
        }
        private void ValidateNgayVL()
        {
            if (NgayVL < NgaySinh)
            {
                NgayVLError = "Ngày vào làm phải lớn hơn ngày sinh!";
                _canAccept[6] = false;
            }
            else
            {
                NgayVLError = "";
                _canAccept[6] = true;
            }
        }
        #endregion
    }
}
