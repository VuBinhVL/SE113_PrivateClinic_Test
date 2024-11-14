using PrivateClinic.View.OtherViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PrivateClinic.Model;
using System.Collections.ObjectModel;

namespace PrivateClinic.ViewModel.OtherViewModels
{
    public class ForgetPasswordViewModel : BaseViewModel
    {
        #region Các property và Command
        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string notification;
        public string Notification
        {
            get => notification;
            set
            {
                if (notification != value)
                {
                    notification = value;
                    OnPropertyChanged(nameof(Notification));
                }
            }
        }
        public ICommand ComeBackLoginCommand {  get; set; }
        public ICommand XacNhanCommand { get; set; }
        #endregion

        private ForgetPasswordView _view;
        //Hàm khởi tạo
        public ForgetPasswordViewModel(ForgetPasswordView view)
        {
            this._view = view;
            ComeBackLoginCommand = new ViewModelCommand(Back);
            XacNhanCommand = new ViewModelCommand(accept);
        }

        //Hàm quay lại login
        private void Back(object obj)
        {
            _view.Close();
        }

        #region Chức năng gửi mật khẩu mới qua email
        private async void accept(object obj)
        {
            Notification = "";

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Email))
            {
                Notification = "Không đủ thông tin!";
                return;
            }

            if (!notify())
            {
                Notification = "Tên tài khoản hoặc Email không tồn tại!";
                return;
            }

            bool mailSent = await Task.Run(() => sendMail(Username, Email));

            if (mailSent)
            {
                Notification = "Mật khẩu đã được gửi tới Email liên kết!";
            }
            else
            {
                Notification = "Có lỗi trong quá trình gửi tới Email liên kết!";
            }

            await Task.Delay(2000);
            Notification = "";
        }



        //Tạo MK mới
        private static string RandomPasswordNew()
        {
            Random random = new Random();
            int passwordLength = random.Next(5, 11);
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var password = new char[passwordLength];

            for (int i = 0; i < passwordLength; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }
            return new string(password);
        }

        //Gửi mật khẩu qua Email
        private static bool sendMail(string username, string mailReceive)
        {
            try
            {
                string fromMail = "privateclinicse104@gmail.com";
                string fromPassWord = "ibap lpjv sqrf vrsq";

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromMail);
                mailMessage.Subject = "Khôi phục mật khẩu";
                mailMessage.To.Add(new MailAddress(mailReceive));


                string noidung = "Mật khẩu mới của bạn là: ";
                string passwordNew = RandomPasswordNew();
                mailMessage.Body = "<html><body>" + noidung + "<b>" + passwordNew + "</b>" + "</body></html>";
                mailMessage.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassWord),
                    EnableSsl = true
                };
                smtpClient.Send(mailMessage);


                //sửa lại password trong bảng account theo username
                ObservableCollection<NGUOIDUNG> list = new ObservableCollection<NGUOIDUNG>(DataProvider.Ins.DB.NGUOIDUNGs);
                foreach (NGUOIDUNG item in list) 
                {
                    if ( username == item.TenDangNhap)
                    {
                        item.MatKhau = passwordNew;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                }
                return true;
            }
            catch { return false; }
        }

        //Báo lỗi tài khoản hoặc email không tồn tại
        private bool notify()
        {
            // Lấy người dùng có tên đăng nhập khớp với Username
            var user = DataProvider.Ins.DB.NGUOIDUNGs.FirstOrDefault(u => u.TenDangNhap == Username);

            if (user != null)
            {
                // Lấy bác sĩ có mã bác sĩ khớp với mã của người dùng và email khớp với Email
                var bs = DataProvider.Ins.DB.BACSIs.FirstOrDefault(b => b.MaBS == user.MaBS && b.Email == Email);

                // Kiểm tra nếu tìm thấy bác sĩ phù hợp
                if (bs != null)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion




    }
}
