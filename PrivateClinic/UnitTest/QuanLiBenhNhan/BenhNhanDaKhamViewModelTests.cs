using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using PrivateClinic.ViewModel.QuanLiKhamBenhVM;
using PrivateClinic.Model;
using Moq;

namespace PrivateClinic.Tests
{
    [TestFixture]
    public class BenhNhanDaKhamViewModelTests
    {
        private BenhNhanDaKhamViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            // Tạo mock data cho các ObservableCollection
            var benhNhans = new ObservableCollection<BENHNHAN>
            {
                new BENHNHAN { MaBN = 1, HoTen = "Nguyen Van A" },
                new BENHNHAN { MaBN = 2, HoTen = "Le Thi B" }
            };

            var loaiBenhs = new ObservableCollection<LOAIBENH>
            {
                new LOAIBENH { MaLoaiBenh = 1, TenLoaiBenh = "Cảm cúm" },
                new LOAIBENH { MaLoaiBenh = 2, TenLoaiBenh = "Sốt xuất huyết" }
            };

            var phieuKhamBenhs = new ObservableCollection<PHIEUKHAMBENH>
            {
                new PHIEUKHAMBENH { MaBN = 1, MaLoaiBenh = 1, TrieuChung = "Ho, sốt", NgayKham = DateTime.UtcNow.Date },
                new PHIEUKHAMBENH { MaBN = 2, MaLoaiBenh = 2, TrieuChung = "Mệt mỏi", NgayKham = DateTime.UtcNow.Date }
            };

            // Tạo instance của ViewModel
            _viewModel = new BenhNhanDaKhamViewModel
            {
                BenhNhan = benhNhans,
                ListLoaiBenh = loaiBenhs,
                ListPKB = phieuKhamBenhs
            };

            _viewModel.LoadData(); // Load dữ liệu vào ViewModel
        }

        [Test]
        public void LoadData_ShouldLoadCorrectData()
        {
            // Act
            _viewModel.LoadData();

            // Assert
            Assert.IsNotNull(_viewModel.ListBN, "ListBN không được null.");
            Assert.IsTrue(_viewModel.ListBN.Count > 0, "ListBN phải có ít nhất một item.");
            Assert.IsTrue(_viewModel.ListBN.All(bn => !string.IsNullOrEmpty(bn.HoTen)), "Tất cả các item phải có họ tên.");
        }

        [Test]
        public void ApplyFilter_FilterByDate_ShouldReturnFilteredResults()
        {
            // Arrange
            _viewModel.FilterDate = DateTime.UtcNow.Date;

            // Act
            _viewModel.ApplyFilter();

            // Assert
            Assert.IsNotNull(_viewModel.FilterListBN, "FilterListBN không được null.");
            Assert.IsTrue(_viewModel.FilterListBN.All(bn => bn.NgayKham.Value.Date == DateTime.UtcNow.Date),
                "Tất cả các item trong FilterListBN phải khớp với ngày được filter.");
        }

        [Test]
        public void ApplyFilter_SearchText_ShouldReturnCorrectResults()
        {
            // Arrange
            _viewModel.SearchText = "Nguyen Van";

            // Act
            _viewModel.ApplyFilter();

            // Assert
            Assert.IsNotNull(_viewModel.FilterListBN, "FilterListBN không được null.");
            Assert.IsTrue(_viewModel.FilterListBN.All(bn => bn.HoTen.Contains("Nguyen Van")),
                "Tất cả các item trong FilterListBN phải chứa 'Nguyen Van'.");
        }

        //[Test]
        //public void SoLuongBNDaKhamHomNay_ShouldReturnCorrectCount()
        //{
        //    // Arrange
        //    _viewModel.FilterDate = DateTime.UtcNow.Date;
        //    _viewModel.ApplyFilter(); // Đảm bảo phương thức ApplyFilter được gọi

        //    // Act
        //    var result = _viewModel.SoLuong;

        //    // Assert
        //    Assert.AreEqual(2, result, "Số lượng bệnh nhân đã khám hôm nay không đúng.");
        //}



        [Test]
        public void ListBN_ShouldUpdate_WhenLoadDataIsCalled()
        {
            // Arrange
            var initialCount = _viewModel.ListBN.Count;

            // Act
            _viewModel.LoadData();

            // Assert
            Assert.AreEqual(initialCount, _viewModel.ListBN.Count, "Số lượng item trong ListBN không được thay đổi sau khi gọi LoadData.");
        }
    }
}
