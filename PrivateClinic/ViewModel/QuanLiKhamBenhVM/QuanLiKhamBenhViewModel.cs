using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhamBenh;
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
using System.Windows.Resources;

namespace PrivateClinic.ViewModel.QuanLiKhamBenhVM
{
    public class QuanLiKhamBenhViewModel : BaseViewModel
    {
        #region Các Property và Command
        private object currentView;
        public object CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        private string chucvu;
        public  string ChucVu { 
            get =>chucvu;
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
        public ICommand SwitchViewCommand { get; set; }
        #endregion
        //Hàm khởi tạo
        public QuanLiKhamBenhViewModel()
        {
            SwitchViewCommand = new ViewModelCommand(SwitchView);
            ThongTinND();
        }

        //Hàm chuyển màn hình
        private void SwitchView(object userControlName)
        {
            string UserControlName = userControlName as string;
            switch (UserControlName)
            {
                case "PatientList":
                    CurrentView = new BenhNhanDaKhamView();
                    break;
            }
        }

        //Hàm load thông tin người dùng và ảnh
        private void ThongTinND()
        {
            string tendangnhap = Const.TenDangNhap;
            User = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == tendangnhap).FirstOrDefault();
            string MaBS = User.MaBS.ToString();
            ObservableCollection<BACSI>DSBS = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSIs);
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
