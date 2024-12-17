using Moq;
using NUnit.Framework;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.ViewModel.HoSoBacSiVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinic.UnitTest.HoSoBacSi
{
	[TestFixture]
	[Apartment(System.Threading.ApartmentState.STA)]
	public class CheckAddDoctor
	{
		private ThemBacSiViewModel _viewModel;

		[SetUp]
		public void SetUp()
		{
			_viewModel = new ThemBacSiViewModel(null);
		}

		[TestCase("Trần Minh Hạ", "")] // Đúng
		[TestCase("", "Họ và tên không được để trống!")] // Tên rỗng
		[TestCase("Hạ #2 Minh", "Họ và tên chỉ chứa kí tự chữ và khoảng trắng!")] // Tên rỗng
		public void CheckName(string hoTen, string expectedError)
		{
			// Arrange
			_viewModel.HoTen = hoTen;

			// Act
			_viewModel.ValidateFullName();

			// Assert
			Assert.That(_viewModel.HoTenError, Is.EqualTo(expectedError));
		}

		[TestCase("vubinh@gmail.com", "")] // Đúng
		[TestCase("", "Email không được để trống!")] // Email rỗng
		[TestCase("123#", "Email không hợp lệ!")] // Tên rỗng
		public void CheckEmail(string email, string expectedError)
		{
			// Arrange
			_viewModel.Email = email;

			// Act
			_viewModel.ValidateEmail();

			// Assert
			Assert.That(_viewModel.EmailError, Is.EqualTo(expectedError));
		}

		[TestCase("0873265725", "")] // Đúng
		[TestCase("0210", "SĐT phải đủ 10 chữ số!")]
		[TestCase("asd%10", "SĐT chỉ chứa số")]
		[TestCase("2876536987", "SĐT phải bắt đầu bằng số 0")]
		[TestCase("", "SĐT không được để trống!")]
		public void CheckSDT(string sDT, string expectedError)
		{
			// Arrange
			_viewModel.SDT = sDT;

			// Act
			_viewModel.ValidateSDT();

			// Assert
			Assert.That(_viewModel.SDTError, Is.EqualTo(expectedError));
		}

		[TestCase("2005-04-24", "")] //Đúng
		[TestCase("2035-12-20", "Ngày sinh không hợp lệ!")] // Ngày sinh không hợp lệ
		[TestCase(null, "Ngày sinh không được để trống!")] // Ngày sinh rỗng
		public void CheckBirthday(string birthDay, string expectedError)
		{
			// Arrange
			DateTime? ngaySinh = null;
			if (!string.IsNullOrWhiteSpace(birthDay))
			{
				ngaySinh = DateTime.Parse(birthDay);
			}
			_viewModel.NgaySinh = ngaySinh;

			// Act
			_viewModel.ValidateBirth();

			// Assert
			Assert.That(_viewModel.NgaySinhError, Is.EqualTo(expectedError));
		}

		[TestCase("2013-03-13", "2005-04-24", "")] //Đúng
		[TestCase("2005-04-24", "2013-03-13", "Ngày vào làm phải lớn hơn ngày sinh!")] //Sai
		[TestCase(null, "2004-07-17", "Ngày vào làm không được để trống!")] // Ngày sinh rỗng
		public void CheckStartDate(string startDate, DateTime birthDay, string expectedError)
		{
			// Arrange
			DateTime? ngayVaoLam = null;
			if (!string.IsNullOrWhiteSpace(startDate))
			{
				ngayVaoLam = DateTime.Parse(startDate);
			}
			_viewModel.NgaySinh = birthDay;
			_viewModel.NgayVL = ngayVaoLam;

			// Act
			_viewModel.ValidateNgayVL();

			// Assert
			Assert.That(_viewModel.NgayVLError, Is.EqualTo(expectedError));
		}

		[TestCase("Đồng Nai", "")]
		[TestCase("", "Địa chỉ không được để trống!")]
		public void CheckAddress(string diachi, string expectedError)
		{
			//Arrange
			_viewModel.DiaChi = diachi;

			// Act
			_viewModel.ValidateDiaChi();
			// Assert
			Assert.That(_viewModel.DiaChiError, Is.EqualTo(expectedError));
		}

		[TestCase("Tiến sĩ", "")]
		[TestCase("", "Bằng cấp không được để trống!")]
		public void CheckDegree(string bangcap, string expectedError)
		{
			//Arrange
			_viewModel.BangCap = bangcap;

			// Act
			_viewModel.ValidateBangCap();
			// Assert
			Assert.That(_viewModel.BangCapError, Is.EqualTo(expectedError));
		}

		[TestCase("Trần Minh Hạ", "0873265725", "vubinh.2004.17.7@gmail.com", "Nam", "2008-04-24", "2013-03-20", "Đồng Nai", "Tiến sĩ", "", "", "", "", "", "", "")]
		[TestCase("Trần Minh Hạ", "210", "vubinh.2004.17.7@gmail.com", "Nam", "2008-04-24", "2013-03-20", "Đồng Nai", "Tiến sĩ", "", "SĐT phải đủ 10 số", "", "", "", "", "")]
		[TestCase("Trần Minh Hạ", "asd%10", "vubinh.2004.17.7@gmail.com", "Nam", "2008-04-24", "2013-03-20", "Đồng Nai", "Tiến sĩ", "", "SĐT chỉ chứa số", "", "", "", "", "")]
		[TestCase("Trần Minh Hạ", "2876536987", "vubinh.2004.17.7@gmail.com", "Nam", "2008-04-24", "2013-03-20", "Đồng Nai", "Tiến sĩ", "", "SĐT phải bắt đầu bằng số 0", "", "", "", "", "")]
		[TestCase("Trần Minh Hạ", "", "vubinh.2004.17.7@gmail.com", "Nam", "2008-04-24", "2013-03-20", "Đồng Nai", "Tiến sĩ", "", "SĐT không được để trống!", "", "", "", "", "")]
		[TestCase("Hạ #2 Minh", "", "vubinh.2004.17.7@gmail.com", "Nam", "2008-04-24", "2013-03-20", "Đồng Nai", "Tiến sĩ", "Họ và tên không chứa kí tự đặc biệt", "", "", "", "", "", "")]
		[TestCase("", "0873265725", "vubinh.2004.17.7@gmail.com", "Nam", "2008-04-24", "2013-03-20", "Đồng Nai", "Tiến sĩ", "Họ và tên không được để trống!", "", "", "", "", "", "")]
		[TestCase("Trần Minh Hạ", "0873265725", "123#", "Nam", "2008-04-24", "2013-03-20", "Đồng Nai", "Tiến sĩ", "", "", "Email không hợp lệ!", "", "", "", "")]
		[TestCase("Trần Minh Hạ", "0873265725", "", "Nam", "2008-04-24", "2013-03-20", "Đồng Nai", "Tiến sĩ", "", "", "Email không được để trống!", "", "", "", "")]
		[TestCase("Trần Minh Hạ", "0873265725", "vubinh.2004.17.7@gmail.com", "Nam", "2035-12-20", "2013-03-20", "Đồng Nai", "Tiến sĩ", "", "", "", "Ngày sinh không hợp lệ!", "Ngày vào làm phải lớn hơn ngày sinh!", "", "")]
		[TestCase("Trần Minh Hạ", "0873265725", "vubinh.2004.17.7@gmail.com", "Nam", null, "2013-03-20", "Đồng Nai", "Tiến sĩ", "", "", "", "Ngày sinh không được để trống!", "", "", "")]
		[TestCase("Trần Minh Hạ", "0873265725", "vubinh.2004.17.7@gmail.com", "Nam", "2008-04-24", "2013-03-20", "Đồng Nai", "Tiến sĩ", "", "", "", "", "", "", "")]
		[TestCase("Trần Minh Hạ", "0873265725", "vubinh.2004.17.7@gmail.com", "Nam", "2008-04-24", null, "Đồng Nai", "Tiến sĩ", "", "", "", "", "Ngày vào làm không được để trống!", "", "")]
		[TestCase("Trần Minh Hạ", "0873265725", "vubinh.2004.17.7@gmail.com", "Nam", "2008-04-24", "2013-03-20", "", "Tiến sĩ", "", "", "", "", "", "Địa chỉ không được để trống!", "")]
		[TestCase("Trần Minh Hạ", "0873265725", "vubinh.2004.17.7@gmail.com", "Nam", "2008-04-24", "2013-03-20", "Đồng Nai", "", "", "", "", "", "", "", "Bằng cấp không được để trống!")]
		public void CheckThemBacSi(string hoten, string sDT, string email, string sex, string birthday,
										string startingday, string diachi, string bangcap,
										string expectedFullNameError, string expectedSDTError,
										string expectedEmailError, string expectedNgaySinhError,
										string expectedNgayVLError, string expectedDiaChiError,
										string expectedBangCapError)
		{
			//Arrange
			_viewModel.HoTen = hoten;
			_viewModel.SDT = sDT;
			_viewModel.Email = email;
			_viewModel.GioiTinh = sex;
			DateTime? ngaysinh = null;
			if (!string.IsNullOrWhiteSpace(birthday))
			{
				ngaysinh = DateTime.Parse(birthday);
			}
			_viewModel.NgaySinh = ngaysinh;
			DateTime? ngayVaoLam = null;
			if (!string.IsNullOrWhiteSpace(startingday))
			{
				ngayVaoLam = DateTime.Parse(startingday);
			}
			_viewModel.NgayVL = ngayVaoLam;
			_viewModel.DiaChi = diachi;
			_viewModel.BangCap = bangcap;

			//Act
			_viewModel.ValidateFullName();
			_viewModel.ValidateSDT();
			_viewModel.ValidateEmail();
			_viewModel.ValidateBirth();
			_viewModel.ValidateNgayVL();
			_viewModel.ValidateDiaChi();
			_viewModel.ValidateBangCap();

			Assert.That(_viewModel.HoTenError, Is.EqualTo(expectedFullNameError));
			Assert.That(_viewModel.SDTError, Is.EqualTo(expectedSDTError));
			Assert.That(_viewModel.EmailError, Is.EqualTo(expectedEmailError));
			Assert.That(_viewModel.NgaySinhError, Is.EqualTo(expectedNgaySinhError));
			Assert.That(_viewModel.NgayVLError, Is.EqualTo(expectedNgayVLError));
			Assert.That(_viewModel.DiaChiError, Is.EqualTo(expectedDiaChiError));
			Assert.That(_viewModel.BangCapError, Is.EqualTo(expectedBangCapError));
		}
	}
}