using Microsoft.Win32;
using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.View.MessageBox;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class ThongTinCaNhanViewModel : BaseViewModel
    {
        #region các property và Command
        public string HoTen {  get; set; }
        public string MaBS { get; set; }
        public string formatMaBS { get; set; }
        public string GioiTinh { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public DateTime? NgaySinh { get; set; }
        public DateTime NgayVL { get; set; }
        public string DiaChi { get; set; }
        public string BangCap { get; set; }
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
        public ICommand editImageCommand { get; set; }
        #endregion
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
        private ObservableCollection<BACSI> dsbs;
        public ObservableCollection<BACSI> DSBS
        {
            get => dsbs;
            set
            {
                dsbs = value;
                OnPropertyChanged(nameof(DSBS));
            }
        }
        public ThongTinCaNhanView _view;
        //Hàm khởi tạo
        HoSoBacSiViewModel hoSoBacSiViewModel;
        public ThongTinCaNhanViewModel(ThongTinCaNhanView view, HoSoBacSiViewModel hoSoBacSi) 
        {
            this.hoSoBacSiViewModel = hoSoBacSi;
            LoadData();
            EditImage();
            this._view = view;
        }
        //Hàm load data lên listview
        void LoadData()
        {
            try
            {
                string tendangnhap = Const.TenDangNhap;
                User = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == tendangnhap).FirstOrDefault();
                MaBS = User.MaBS.ToString();
                DSBS = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSIs);
                foreach (BACSI bac in DSBS)
                {
                    if (bac.MaBS.ToString() == MaBS)
                    {
                        FormatMaBS();
                        formatMaBS = bac.formatMaBS;
                        HoTen = bac.HoTen;
                        GioiTinh = bac.GioiTinh;
                        SDT = bac.SDT;
                        Email = bac.Email;
                        NgaySinh = bac.NgaySinh;
                        NgayVL = bac.NgayVaoLam;
                        DiaChi = bac.DiaChi;
                        BangCap = bac.BangCap;
                        if (bac.Image == null)
                        {
                            Uri resourceUri = new Uri("pack://application:,,,/ResourceXAML/image/img_default.png", UriKind.Absolute);
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
                            BitmapImage image = ImageViewModel.ByteArrayToBitmapImage(bac.Image);
                            ImageSource = image;
                        }
                        break;
                    }
                }
            }
            catch { }
           
        }

        // Hàm định dạng lại mã bác sĩ đưa lên view
        void FormatMaBS()
        {
            foreach (var item in DSBS)
            {
                if (item.MaBS < 10)
                {
                    item.formatMaBS = "BS00" + item.MaBS;
                }
                else if (item.MaBS < 100)
                {
                    item.formatMaBS = "BS0" + item.MaBS;
                }
                else
                    item.formatMaBS = "BS" + item.MaBS;
            }
        }

        #region Chức năng thay đổi ảnh đại diện
        private void EditImage()
        {
            editImageCommand = new ViewModelCommand(editImage);
        }
        // Hàm thực hiện cập nhật ảnh đại diện cho tài khoản
        private void editImage(object obj)
        {
            ChooseImage();
            //Lấy thông tin bác sĩ đang dùng app
            string tendangnhap = Const.TenDangNhap;
            User = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == tendangnhap).FirstOrDefault();
            MaBS = User.MaBS.ToString();
            DSBS = new ObservableCollection<BACSI>(DataProvider.Ins.DB.BACSIs);
            foreach (var item in DSBS)
            {
                if (item.MaBS.ToString() == MaBS) 
                {
                    byte[] imageBytes = ImageViewModel.BitmapImageToByteArray(imageSource);
                    item.Image = imageBytes;
                    DataProvider.Ins.DB.SaveChanges();
                    hoSoBacSiViewModel.ImageSource =ImageViewModel.ByteArrayToBitmapImage(imageBytes);
                    break;
                }
            }
        }

        //Hàm giúp chọn ảnh từ máy
        private void ChooseImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";

            byte[] imageData;

            if (openFileDialog.ShowDialog() == true)
            {
                imageData = File.ReadAllBytes(openFileDialog.FileName);

                ImageSource = ImageViewModel.ByteArrayToBitmapImage(imageData);
                OkMessageBox mb = new OkMessageBox("Thông báo", "Đổi ảnh đại diện thành công");
                mb.ShowDialog();
            }

        }
        #endregion

    }

}

