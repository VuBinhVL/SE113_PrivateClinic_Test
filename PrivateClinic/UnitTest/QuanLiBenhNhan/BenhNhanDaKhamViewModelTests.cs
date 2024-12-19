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


        // Kiểm tra filter theo ngày với các trường hợp khác nhau
        [TestCase("2024-12-16", true)] // Ngày khớp
        public void ApplyFilter_FilterByDate_ShouldReturnFilteredResults(string filterDate, bool expectedMatch)
        {
            // Arrange
            _viewModel.FilterDate = DateTime.Parse(filterDate).Date;

            // Act
            _viewModel.ApplyFilter();

            // Assert
            Assert.IsNotNull(_viewModel.FilterListBN, "FilterListBN không được null.");
            bool isMatch = _viewModel.FilterListBN.All(bn => bn.NgayKham.HasValue && bn.NgayKham.Value.Date == DateTime.Parse(filterDate).Date);
          

            Assert.That(isMatch, Is.EqualTo(expectedMatch), "Kết quả filter theo ngày không chính xác.");

            Assert.That(isMatch, Is.EqualTo(expectedMatch), "Kết quả filter theo ngày không chính xác.");
        }

        // Kiểm tra filter theo từ khóa tìm kiếm với các trường hợp khác nhau
        [TestCase("Nguyen Van", true)] // Tìm thấy kết quả
        public void ApplyFilter_SearchText_ShouldReturnCorrectResults(string searchText, bool expectedMatch)
        {
            // Arrange
            _viewModel.SearchText = searchText;

            // Act
            _viewModel.ApplyFilter();

            // Assert
            Assert.IsNotNull(_viewModel.FilterListBN, "FilterListBN không được null.");
            bool isMatch = expectedMatch
                ? _viewModel.FilterListBN.Any(bn => bn.HoTen.Contains(searchText))
                : !_viewModel.FilterListBN.Any();
            Assert.That(isMatch, Is.EqualTo(expectedMatch), "Kết quả filter theo từ khóa không chính xác.");
        }




    }
}
