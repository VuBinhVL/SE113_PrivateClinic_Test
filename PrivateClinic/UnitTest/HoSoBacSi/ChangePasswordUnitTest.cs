using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using PrivateClinic.Model;
using PrivateClinic.ViewModel.HoSoBacSiVM;
using PrivateClinic.View.HoSoBacSi;
using System.Windows;

namespace PrivateClinic.Tests
{
	[TestFixture]
	[Apartment(System.Threading.ApartmentState.STA)]
	public class DoiMatKhauViewModelTests
	{
		private DoiMatKhauViewModel _viewModel;
		private NGUOIDUNG _users;

		[Apartment(System.Threading.ApartmentState.STA)]
		[SetUp]
		public void Setup()
		{
			_users = new NGUOIDUNG
			{
				TenDangNhap = "testuser",
				MatKhau = "123456" // Mật khẩu cũ
			};
			_viewModel = new DoiMatKhauViewModel(null)
			{
				User = _users
			};
		}

		[TestCase("123456", "Binh2k4", "testing", "Mật khẩu mới không khớp!")]
		[TestCase("asd", "Binh2k4", "testing", "Mật khẩu cũ không đúng!")]
		[TestCase("", "Binh2k4", "testing", "Không đủ thông tin!")]
		[TestCase("123456", "testing", "testing", "")]
		[TestCase("123456", "", "Binh@2k4", "Không đủ thông tin!")]
		[TestCase("123456", "Binh2k4", "testing", "Mật khẩu mới không khớp!")]
		[TestCase("123456", "Binh2k4", "", "Không đủ thông tin!")]
		public void DoiMatKhau(string mkcu, string mkm1, string mkm2, string expectedError)
		{
			//Arrage
			_viewModel.Matkhaucu = mkcu;
			_viewModel.Matkhaumoi1 = mkm1;
			_viewModel.Matkhaumoi2 = mkm2;

			//Act
			_viewModel.ChangePass();

			//Assert
			Assert.That(_viewModel.Error, Is.EqualTo(expectedError));
		}
	}
}