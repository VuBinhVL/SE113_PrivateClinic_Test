using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.View.MessageBox;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
    public class ThongTinChiTietCuaMotBacSiViewModel
    {
        #region các property
        public int MaBS {  get; set; }
        public string formatMaBS { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public DateTime?NgaySinh { get; set; }
        public DateTime NgayVL { get; set; }
        public string DiaChi { get; set; }
        public string BangCap { get; set; }
        public BitmapImage ImageSource { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ThongTinChiTietCuaMotBacSIView _view;
        DanhSachBacSiViewModel _viewModel {  get; set; }
        #endregion

        //Hàm khởi tạo
        public ThongTinChiTietCuaMotBacSiViewModel(BACSI bs,ThongTinChiTietCuaMotBacSIView view) 
        {
            HoTen = bs.HoTen;
            MaBS = bs.MaBS;
            formatMaBS = bs.formatMaBS;
            GioiTinh = bs.GioiTinh;
            SDT = bs.SDT;
            Email = bs.Email;
            NgaySinh = bs.NgaySinh;
            NgayVL = bs.NgayVaoLam;
            DiaChi = bs.DiaChi;
            BangCap = bs.BangCap;
            if (bs.Image == null)
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
                BitmapImage image = ImageViewModel.ByteArrayToBitmapImage(bs.Image);
                ImageSource = image;
            }
            ExitCommand = new RelayCommand<ThongTinChiTietCuaMotBacSIView>((p) => true, (p) => Close(p));
            EditCommand = new RelayCommand<ThongTinChiTietCuaMotBacSIView>((p) => { return p == null ? false : true; }, (p) => _EditCommand(p));
            SaveCommand = new RelayCommand<ThongTinChiTietCuaMotBacSIView>((p) => { return p == null ? false : true; }, (p) => _SaveCommand(p));
            DeleteCommand = new RelayCommand<ThongTinChiTietCuaMotBacSIView>((p) => { return p == null ? false : true; }, (p) => _DeleteCommand(p));
            this._view = view;

        }

        //Hàm hỗ trợ nút thoát
        void Close(ThongTinChiTietCuaMotBacSIView p)
        {
            p.Close();
        }

        //Cho phép sửa
        void _EditCommand(ThongTinChiTietCuaMotBacSIView parameter)
        {

            parameter.Ngaysinhtxb.IsEnabled = true;
            parameter.NgayVaoLamtxb.IsEnabled = true;
            parameter.HoTentxb.IsReadOnly = false;
            parameter.GioiTinhtxb.IsEnabled = true;
            parameter.Emailtxb.IsReadOnly = false;
            parameter.DiaChitxb.IsReadOnly = false;
            parameter.SDTtxb.IsReadOnly = false;
            parameter.Emailtxb.IsReadOnly = false;
            parameter.BangCaptxb.IsReadOnly = false;
            parameter.btnSave.Visibility = System.Windows.Visibility.Visible;
            parameter.btnSave.IsEnabled = true;

        }

        //Lưu thông tin bác sĩ sau khi sửa
        void _SaveCommand(ThongTinChiTietCuaMotBacSIView parameter)
        {
            BACSI t = DataProvider.Ins.DB.BACSIs.SingleOrDefault(h => h.MaBS == MaBS);

            YesNoMessageBox result = new YesNoMessageBox("Thông báo", "Bạn có muốn lưu lại thông tin bác sĩ không?");
            result.ShowDialog();
            if (result.DialogResult == true)
            {
                if (parameter.SDTtxb.Text == "" || parameter.Emailtxb.Text == "" || parameter.BangCaptxb.Text == "" ||
                       parameter.DiaChitxb.Text == "" || parameter.HoTentxb.Text == "" || parameter.Ngaysinhtxb.SelectedDate == null || parameter.GioiTinhtxb.SelectedValue==null
                       || parameter.NgayVaoLamtxb.SelectedDate == null)
                {
                    OkMessageBox mb = new OkMessageBox("Thông báo","Nhập thiếu thông tin!");
                    mb.ShowDialog();
                }
                else if (!parameter.Emailtxb.Text.Contains("@"))
                {
                    OkMessageBox mb = new OkMessageBox("Thông báo", "Email không hợp lệ!");
                    mb.ShowDialog();
                }
                else if (!parameter.SDTtxb.Text.All(char.IsDigit))
                {
                    OkMessageBox mb = new OkMessageBox("Thông báo", "Số điện thoại chỉ chứa số!");
                    mb.ShowDialog();
                }
                else if (parameter.Ngaysinhtxb.SelectedDate.Value > DateTime.UtcNow)
                {
                    OkMessageBox mb = new OkMessageBox("Thông báo", "Ngày sinh không hợp lệ!");
                    mb.ShowDialog();
                }
                else if (parameter.NgayVaoLamtxb.SelectedDate.Value <  parameter.Ngaysinhtxb.SelectedDate)
                {
                    OkMessageBox mb = new OkMessageBox("Thông báo", "Ngày vào làm phải lớn hơn ngày sinh!");
                    mb.ShowDialog();
                }
                else
                {
                    t.HoTen = parameter.HoTentxb.Text;
                    t.SDT = parameter.SDTtxb.Text;
                    t.Email = parameter.Emailtxb.Text;
                    t.BangCap = parameter.BangCaptxb.Text;
                    t.NgayVaoLam = parameter.NgayVaoLamtxb.SelectedDate.Value;
                    t.NgaySinh = parameter.Ngaysinhtxb.SelectedDate;
                    t.GioiTinh = parameter.GioiTinhtxb.SelectedValue.ToString();
                    t.DiaChi = parameter.DiaChitxb.Text;
                    DataProvider.Ins.DB.SaveChanges();
                    OkMessageBox mb = new OkMessageBox("Thông báo", "Lưu thông tin thành công!");
                    mb.ShowDialog();
                    //Ẩn các thông tin
                    parameter.btnSave.Visibility = Visibility.Hidden;
                    parameter.Ngaysinhtxb.IsEnabled = false;
                    parameter.NgayVaoLamtxb.IsEnabled = false;
                    parameter.HoTentxb.IsReadOnly = true;
                    parameter.GioiTinhtxb.IsEnabled = false;
                    parameter.Emailtxb.IsReadOnly = true;
                    parameter.DiaChitxb.IsReadOnly = true;
                    parameter.SDTtxb.IsReadOnly = true;
                    parameter.Emailtxb.IsReadOnly = true;
                    parameter.BangCaptxb.IsReadOnly = true;
                }
                
            }
        }

        //Xóa thông tin bác sĩ
        void _DeleteCommand(ThongTinChiTietCuaMotBacSIView parameter)
        {
            BACSI selectedItem = DataProvider.Ins.DB.BACSIs.SingleOrDefault(s => s.MaBS == MaBS);
            YesNoMessageBox mb = new YesNoMessageBox("Thông báo", "Bạn có muốn xóa bác sĩ này không?");
            mb.ShowDialog();
            if (mb.DialogResult == true)
            {
                if (selectedItem != null && selectedItem.MaBS != 1)
                {
                    //Xóa tài khoàn trước
                    var taikhoan = DataProvider.Ins.DB.NGUOIDUNGs.Where(h => h.MaBS == selectedItem.MaBS).ToList();
                    DataProvider.Ins.DB.NGUOIDUNGs.RemoveRange(taikhoan);
                    //Cập nhật mã bác sĩ trong các phiếu khám bệnh bằng NULL
                    var phieukhambenh = DataProvider.Ins.DB.PHIEUKHAMBENHs.Where(h => h.MaBS == selectedItem.MaBS).ToList();
                    foreach (var h in phieukhambenh)
                        h.MaBS = null;
                    //Xóa bác sĩ
                    DataProvider.Ins.DB.BACSIs.Remove(selectedItem);

                    //Lưu xuống CSDL
                    DataProvider.Ins.DB.SaveChanges();
                    //Cập nhật listview


                    OkMessageBox mbs = new OkMessageBox("Thông báo", "Xóa thành công");
                    mbs.ShowDialog(); 
                    parameter.Close();
                }
                else
                {
                    OkMessageBox mbs = new OkMessageBox("Thông báo", "Không thể xóa chính mình");
                    mbs.ShowDialog();
                }
            }
            
            
        }
        
        

    }
}
