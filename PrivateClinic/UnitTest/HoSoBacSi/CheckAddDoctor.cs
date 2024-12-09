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
	}
}