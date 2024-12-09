using Moq;
using NUnit.Framework;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.ViewModel.HoSoBacSiVM;
using System;
using System.Collections.Generic;
using System.Linq;
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
	}
}