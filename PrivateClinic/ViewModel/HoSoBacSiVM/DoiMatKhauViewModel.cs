using PrivateClinic.Model;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.View.MessageBox;
using PrivateClinic.ViewModel.OtherViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace PrivateClinic.ViewModel.HoSoBacSiVM
{
	public class DoiMatKhauViewModel : BaseViewModel
	{
		#region Các Property và Command

		public ICommand ChangePasswordCommand { get; set; }

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

		private string matkhaucu;
		public string Matkhaucu { get; set; }

		private string matkhaumoi1;
		public string Matkhaumoi1 { get; set; }

		private string matkhaumoi2;
		public string Matkhaumoi2 { get; set; }

		private string error;
		public string Error { get; set; }

		#endregion Các Property và Command

		private DoiMatKhauView _view;

		//Hàm khởi tạo
		public DoiMatKhauViewModel(DoiMatKhauView view)
		{
			ChangePasswordCommand = new RelayCommand<DoiMatKhauView>((p) => true, (p) => changePassword(p));
			this._view = view;
		}

		//Hàm thay đổi pass
		private void changePassword(object obj)
		{
			//Lấy tên đăng nhập hiện tại của user
			string tendangnhap = Const.TenDangNhap;
			//Lấy đối tượng đang dùng app
			User = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == tendangnhap).FirstOrDefault();
			if (_view.txtMKmoi.Password.ToString() == "" || _view.txtNhapLai.Password.ToString() == " " || _view.txtMKcu.Password.ToString() == " ")
			{
				OkMessageBox mb = new OkMessageBox("Thông báo", "Chưa nhập đủ thông tin");
				mb.ShowDialog();
			}
			else if (_view.txtMKcu.Password.ToString() != User.MatKhau)
			{
				OkMessageBox mb = new OkMessageBox("Thông báo", "Mật khẩu cũ không đúng");
				mb.ShowDialog();
			}
			else if (_view.txtMKmoi.Password.ToString().Contains(" "))
			{
				OkMessageBox mb = new OkMessageBox("Thông báo", "Mật khẩu không được chứa khoảng trắng");
				mb.ShowDialog();
			}
			else if (_view.txtMKmoi.Password.ToString() != _view.txtNhapLai.Password.ToString())
			{
				OkMessageBox mb = new OkMessageBox("Thông báo", "Mật khẩu mới không khớp");
				mb.ShowDialog();
			}
			else
			{
				User.MatKhau = _view.txtMKmoi.Password.ToString();
				DataProvider.Ins.DB.SaveChanges();
				OkMessageBox mb = new OkMessageBox("Thông báo", "Đổi mật khẩu thành công");
				mb.ShowDialog();
				_view.txtMKcu.Password = "";
				_view.txtNhapLai.Password = "";
				_view.txtMKmoi.Password = "";
			}
		}

		#region hàm phục vụ việc test

		public void ChangePass()
		{
			if (Matkhaumoi1 == "" || Matkhaumoi2 == "" || Matkhaucu == "")
			{
				Error = "Không đủ thông tin!";
			}
			else if (Matkhaucu != User.MatKhau)
			{
				Error = "Mật khẩu cũ không đúng!";
			}
			else if (Matkhaumoi1 != Matkhaumoi2)
			{
				Error = "Mật khẩu mới không khớp!";
			}
			else
			{
				Error = "";
			}
		}

		#endregion hàm phục vụ việc test
	}
}