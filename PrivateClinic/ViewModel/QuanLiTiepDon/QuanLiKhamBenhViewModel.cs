using PrivateClinic.ViewModel.OtherViewModels;
using PrivateClinic.View.QuanLiTiepDon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Documents;
using PrivateClinic.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Resources;

namespace PrivateClinic.ViewModel.QuanLiTiepDon
{
    public class QuanLiKhamBenhViewModel : BaseViewModel
    {
        private UserControl currentView;
        public UserControl CurrentView
        {
            get => currentView;
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
        public ICommand KhamBNCommand { get; set; }
        public ICommand SwitchViewCommand { get; set; }


        public QuanLiKhamBenhViewModel()
        {
            SwitchViewCommand = new ViewModelCommand(SwitchView);
            ThongTinND();
        }

        private void SwitchView(object userControlName)
        {
            string userControlNameStr = userControlName as string;
            switch (userControlNameStr)
            {
                case "BNdangkham":
                    CurrentView = new BenhNhanDangKhamView();
                    break;
                case "DSBenhNhan":
                    CurrentView = new QuanLiTiepDonView();
                    break;
            }
        }
        private void KhamBNCommandExecute(object parameter)
        {
            CurrentView = new BenhNhanDangKhamView();
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