using Moq;
using NUnit.Framework;
using PrivateClinic.Model;
using PrivateClinic.View.MessageBox;
using PrivateClinic.ViewModel.ThanhToan;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinic.UnitTest.HoaDon
{
    [TestFixture]
    [Apartment(System.Threading.ApartmentState.STA)]
    public class XoaHoaDonChuaThanhToanViewModelTest
    {
        private ThanhToanViewModel _viewModel;
        private ObservableCollection<HOADON> _hoaDons;
        private ObservableCollection<CT_BCDT> _ctBcDt;
        private ObservableCollection<CT_PKB> _ctPkb;
        private ObservableCollection<PHIEUKHAMBENH> _phieuKhb;

        [SetUp]
        public void SetUp()
        {
            // Tạo dữ liệu giả lập
            _hoaDons = new ObservableCollection<HOADON>
        {
            new HOADON { SoHD = 1, MaPKB = 101, TrangThai = "0" },
            new HOADON { SoHD = 2, MaPKB = 102, TrangThai = "1" }
        };

            _ctBcDt = new ObservableCollection<CT_BCDT>
        {
            new CT_BCDT { SoHD = 1 },
            new CT_BCDT { SoHD = 1 }
        };

            _ctPkb = new ObservableCollection<CT_PKB>
        {
            new CT_PKB { MaPKB = 101 }
        };

            _phieuKhb = new ObservableCollection<PHIEUKHAMBENH>
        {
            new PHIEUKHAMBENH { MaPKB = 101 }
        };

            // Khởi tạo ViewModel
            _viewModel = new ThanhToanViewModel
            {
                listHD = _hoaDons
            };
        }

        [Test]
        public void DeleteCommand_Success()
        {
            // Arrange
            var selectedHoaDon = _hoaDons.First();
           
            // Act
            _viewModel._DeleteCommand(selectedHoaDon);

            // Assert
            Assert.IsFalse(_viewModel.listHD.Contains(selectedHoaDon), "Hóa đơn không được xóa khỏi danh sách.");
            Assert.AreEqual(0, _ctBcDt.Count(ct => ct.SoHD == selectedHoaDon.SoHD), "Các CT_BCDT liên quan không được xóa.");
            Assert.AreEqual(0, _ctPkb.Count(ct => ct.MaPKB == selectedHoaDon.MaPKB), "Các CT_PKB liên quan không được xóa.");
            Assert.AreEqual(0, _phieuKhb.Count(pkb => pkb.MaPKB == selectedHoaDon.MaPKB), "Các PHIEUKHAMBENH liên quan không được xóa.");
        }
    }
}
