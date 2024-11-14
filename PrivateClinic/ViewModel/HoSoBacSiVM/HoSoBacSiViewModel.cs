using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Resources;
using System.Xml.Linq;
using System.Windows;
using PrivateClinic.View.QuanLiKhoThuoc;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class HoSoBacSiViewModel : BaseViewModel
    {
        #region Các Property và Command
        private object currentView;
        public object CurrentView
        {
            get
            {
                return currentView;
            }
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        private string chucvu;
        public string ChucVu
        {
            get => chucvu;
            set
            {
                chucvu = value;
                OnPropertyChanged(nameof(ChucVu));
            }
        }
        private string tenNV;
        public string TenNV
        {
            get => tenNV;
            set
            {
                tenNV = value;
                OnPropertyChanged(nameof(TenNV));
            }
        }
        private NGUOIDUNG user;
        public NGUOIDUNG User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        private BitmapImage imageSource;
        public BitmapImage ImageSource
        {
            get => imageSource;
            set
            {
                imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        public ICommand SwitchViewCommand { get; private set; }
        #endregion

        private NGUOIDUNG _NhanVien;
        public NGUOIDUNG NhanVien { get => _NhanVien; set { _NhanVien = value; OnPropertyChanged(); } }

        private Visibility _SetQuanLy;
        public Visibility SetQuanLy { get => _SetQuanLy; set { _SetQuanLy = value; OnPropertyChanged(); } }
        private Visibility _SetNhanVien;
        public Visibility SetNhanVien { get => _SetNhanVien; set { _SetNhanVien = value; OnPropertyChanged(); } }
        public ICommand LoadCommand { get; set; }
        public HoSoBacSiViewModel()
        {
            LoadCommand = new RelayCommand<HoSoBacSiView>((p) => true, (p) => _LoadCommand(p));
            SwitchViewCommand = new ViewModelCommand(SwitchView);
            ThongTinND();
        }
        private void _LoadCommand(HoSoBacSiView p)
        {
            if(Const.PQ.MaNhom== 1)
            {
                SetQuanLy = Visibility.Visible;
                SetNhanVien = Visibility.Collapsed;
                
            }
            else
            {
                SetQuanLy = Visibility.Collapsed;
                SetNhanVien = Visibility.Visible;

            }
        }
        private void SwitchView(object userControlName)
        {
            string UserControlName = userControlName as string;
            switch(UserControlName)
            {
                case "Personal":
                    CurrentView = new ThongTinCaNhanView( this);
                    break;
                case "DoctorList":
                    CurrentView = new DanhSachBacSiView();
                    break;
                case "PQ":
                    CurrentView = new PhanQuyenUS();
                    break;
                case "Password":
                    CurrentView = new DoiMatKhauView();
                    break;


            }
        }
        //Hàm load thông tin người dùng và ảnh
        private void ThongTinND()
        {
            string tendangnhap = Const.TenDangNhap;
            User = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == tendangnhap).FirstOrDefault();
            string MaBS = User.MaBS.ToString();
            ObservableCollection<BACSI> DSBS = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSIs);
            if (Const.PQ.MaNhom == 1)
                ChucVu = "Admin";
            else
                ChucVu = "Nhân viên";
            foreach (BACSI bs in DSBS)
            {
                if (bs.MaBS.ToString() == MaBS)
                {
                    TenNV = bs.HoTen;
                    if (bs.Image == null)
                    {
                        Uri resourceUri = new Uri("pack://application:,,,/ResourceXAML/image/ic_male_user_blue.png", UriKind.Absolute);
                        StreamResourceInfo streamInfo = System.Windows.Application.GetResourceStream(resourceUri);
                        byte[] imageBytes;
                        using (Stream imageStream = streamInfo.Stream)
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                imageStream.CopyTo(ms);
                                imageBytes = ms.ToArray();
                            }
                        }
                        ImageSource = ImageViewModel.ByteArrayToBitmapImage(imageBytes);
                    }
                    else
                    {
                        BitmapImage image = ImageViewModel.ByteArrayToBitmapImage(bs.Image);
                        ImageSource = image;
                    }
                    break;
                }
            }
        }
    }
}
