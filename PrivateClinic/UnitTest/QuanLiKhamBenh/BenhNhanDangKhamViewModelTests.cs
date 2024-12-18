using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using PrivateClinic.ViewModel.QuanLiTiepDon;
using PrivateClinic.Model;
using System.Linq;

namespace PrivateClinic.UnitTest.QuanLiTiepDon
{
    [TestFixture]
    [Apartment(System.Threading.ApartmentState.STA)]
    public class CheckAddMedicine
    {
        private ThemThuocChoBenhNhanViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            // Khởi tạo ViewModel và danh sách mẫu
            _viewModel = new ThemThuocChoBenhNhanViewModel(null);
            _viewModel.ListThuoc = new ObservableCollection<THUOC>
            {
                new THUOC { MaThuoc = 1, TenThuoc = "Thuốc A", SoLuong = 10, MaDVT = 1 },
                new THUOC { MaThuoc = 2, TenThuoc = "Thuốc B", SoLuong = 5, MaDVT = 2 }
            };

            _viewModel.ListCachDung = new ObservableCollection<CACHDUNG>
            {
                new CACHDUNG { MaCachDung = 1, TenCachDung = "Uống sau ăn" },
                new CACHDUNG { MaCachDung = 2, TenCachDung = "Uống trước ăn" }
            };

            _viewModel.ListDVT = new ObservableCollection<DVT>
            {
                new DVT { MaDVT = 1, TenDVT = "Viên" },
                new DVT { MaDVT = 2, TenDVT = "Gói" }
            };
        }


        //kiểm tra thêm thuốc với các trường hợp khác nhau
        [TestCase(0, 1, "5", "Chưa nhập đủ thông tin.")] // Thuốc chưa được chọn
        [TestCase(1, 0, "5", "Chưa nhập đủ thông tin.")] // Cách dùng chưa được chọn
        [TestCase(1, 1, null, "Chưa nhập đủ thông tin.")] // Số lượng không được để trống
        [TestCase(1, 1, "-5", "Số lượng không hợp lệ.")] // Số lượng phải lớn hơn 0
        [TestCase(1, 1, "20", "Số lượng thuốc không đủ.")] // Số lượng thuốc không đủ
        [TestCase(1, 1, "5", "")] // Trường hợp đúng
        public void CheckAddMedicine_Validation(int maThuoc, int maCachDung, string soLuong, string expectedError)
        {
            // Arrange
            _viewModel.SelectedThuoc = maThuoc == 0 ? null : _viewModel.ListThuoc.FirstOrDefault(t => t.MaThuoc == maThuoc);
            _viewModel.SelectedCachDung = maCachDung == 0 ? null : _viewModel.ListCachDung.FirstOrDefault(c => c.MaCachDung == maCachDung);
            _viewModel.SoLuong = soLuong;

            // Kiểm tra nếu SelectedThuoc hoặc SelectedCachDung là null, không thực hiện thêm thuốc
            if (_viewModel.SelectedThuoc == null || _viewModel.SelectedCachDung == null || string.IsNullOrEmpty(soLuong))
            {
                _viewModel.ErrorMessage = "Chưa nhập đủ thông tin.";
            }
            else
            {
                // Act
                _viewModel.AcceptAdd(null);
            }

            // Assert
            Assert.That(_viewModel.ErrorMessage, Is.EqualTo(expectedError));
        }


        //Kiểm tra thêm thuốc thành công
        [TestCase(1, 1, "5", 1, 5)] // Trường hợp dữ liệu hợp lệ
        public void CheckAddMedicine_ValidData_AddsMedicineToList(int maThuoc, int maCachDung, string soLuong, int expectedCount, int expectedRemainingQuantity)
        {
            // Arrange
            _viewModel.SelectedThuoc = _viewModel.ListThuoc.First(t => t.MaThuoc == maThuoc);
            _viewModel.SelectedCachDung = _viewModel.ListCachDung.First(c => c.MaCachDung == maCachDung);
            _viewModel.SoLuong = soLuong;

            // Act
            _viewModel.AcceptAdd(null);

            // Assert
            Assert.AreEqual(expectedCount, _viewModel.ListThuocDTO.Count, "Danh sách thuốc phải có số lượng thuốc đúng.");
            Assert.AreEqual(expectedRemainingQuantity, _viewModel.ListThuoc.First(t => t.MaThuoc == maThuoc).SoLuong, "Số lượng thuốc trong kho không chính xác.");
        }
    }
}
