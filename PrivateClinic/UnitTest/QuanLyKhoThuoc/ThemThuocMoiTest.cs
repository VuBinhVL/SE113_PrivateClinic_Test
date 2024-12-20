using NUnit.Framework;
using PrivateClinic.Model;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.ViewModel.QuanLiKhamBenhVM;
using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrivateClinic.UnitTest.QuanLyKhoThuoc
{
    [TestFixture]
    [Apartment(System.Threading.ApartmentState.STA)]
    public class ThemThuocMoiTest
    {
        private ThemThuocMoiView _view;
        private ThemThuocMoiViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {

            // Đảm bảo Application.Current không null
            if (Application.Current == null)
            {
                new Application();
            }

            // Tạo ResourceDictionary giả lập chứa tài nguyên icStethoscope
            var resourceDictionary = new ResourceDictionary();
            var drawing = new System.Windows.Media.DrawingImage(
      new System.Windows.Media.GeometryDrawing(
          System.Windows.Media.Brushes.Blue,
          new System.Windows.Media.Pen(System.Windows.Media.Brushes.Black, 1),
          new System.Windows.Media.EllipseGeometry(new System.Windows.Point(10, 10), 10, 10)
      )
  );
            resourceDictionary["icStethoscope"] = drawing; 

            // Thêm ResourceDictionary vào Application.Current.Resources
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

            // Tạo mock data cho ObservableCollection
            var dvts = new ObservableCollection<DVT>
        {
            new DVT {MaDVT = 1, TenDVT = "Viên"},
            new DVT {MaDVT = 2, TenDVT = "Chai"}
        };

            var cachDungs = new ObservableCollection<CACHDUNG>
        {
            new CACHDUNG {MaCachDung = 1, TenCachDung = "Uống trước khi ăn"},
            new CACHDUNG {MaCachDung = 2, TenCachDung = "Uống sau khi ăn"}
        };

            var loaiThuocs = new ObservableCollection<LOAITHUOC>
        {
            new LOAITHUOC {MaLoaiThuoc = 1, TenLoaiThuoc = "Thuốc đau đầu"},
            new LOAITHUOC {MaLoaiThuoc = 2, TenLoaiThuoc = "Thuốc hạ sốt"}
        };

            // Tạo instance của ViewModel
            _viewModel = new ThemThuocMoiViewModel
            {
                donvitinh = dvts,
                cachdung = cachDungs,
                loaithuoc = loaiThuocs
            };

            // Khởi tạo mock view
            _view = new ThemThuocMoiView
            {
                SL = new System.Windows.Controls.TextBox(),
                DonGiaNhap = new System.Windows.Controls.TextBox(),
                MaThuoc = new System.Windows.Controls.TextBox(),
                TenThuoc = new System.Windows.Controls.TextBox(),
                TenDVTcbx = new System.Windows.Controls.ComboBox(),
                LoaiThuoccbx = new System.Windows.Controls.ComboBox(),
                CachDungcbx = new System.Windows.Controls.ComboBox(),
                NgayNhap = new System.Windows.Controls.DatePicker()
            };

            // Gán dữ liệu mẫu cho ComboBox
            _view.TenDVTcbx.ItemsSource = dvts.Select(x => x.TenDVT).ToList();
            _view.LoaiThuoccbx.ItemsSource = loaiThuocs.Select(x => x.TenLoaiThuoc).ToList();
            _view.CachDungcbx.ItemsSource = cachDungs.Select(x => x.TenCachDung).ToList();
        }

        [Test]
        public void AddMedication_Success()
        {
            // Set attributes
            _view.SL.Text = "10";
            _view.DonGiaNhap.Text = "5000";
            _view.MaThuoc.Text = "M001";
            _view.TenThuoc.Text = "Paracetamol";
            _view.TenDVTcbx.SelectedItem = "Viên";
            _view.LoaiThuoccbx.SelectedItem = "Thuốc đau đầu";
            _view.CachDungcbx.SelectedItem = "Uống trước khi ăn";
            _view.NgayNhap.SelectedDate = DateTime.Now;

            // Act
            _viewModel._SaveCommand(_view);

            // Assert
            var addedMedication = _viewModel.Thuoc.FirstOrDefault(t => t.TenThuoc == "Paracetamol");
            Assert.IsNotNull(addedMedication, "Thuốc không được thêm vào cơ sở dữ liệu");
            Assert.AreEqual(10, addedMedication.SoLuong, "Số lượng thuốc không đúng");
            Assert.AreEqual(5000, addedMedication.DonGiaNhap, "Đơn giá nhập không đúng");
            Assert.AreEqual("Viên", _viewModel.donvitinh.FirstOrDefault(dvt => dvt.MaDVT == addedMedication.MaDVT)?.TenDVT, "Đơn vị tính không đúng");
            Assert.AreEqual("Thuốc đau đầu", _viewModel.loaithuoc.FirstOrDefault(lt => lt.MaLoaiThuoc == addedMedication.MaLoaiThuoc)?.TenLoaiThuoc, "Loại thuốc không đúng");
            Assert.AreEqual("Uống trước khi ăn", _viewModel.cachdung.FirstOrDefault(cd => cd.MaCachDung == addedMedication.MaCachDung)?.TenCachDung, "Cách dùng không đúng");
        }

        [Test]
        public void AddMedication_Failure_MissingData()
        {
            // Set attributes with missing data
            _view.SL.Text = ""; // Số lượng trống
            _view.DonGiaNhap.Text = "5000";
            _view.MaThuoc.Text = "M001";
            _view.TenThuoc.Text = "Paracetamol";
            _view.TenDVTcbx.SelectedItem = "Viên";
            _view.LoaiThuoccbx.SelectedItem = "Thuốc đau đầu";
            _view.CachDungcbx.SelectedItem = "Uống trước khi ăn";
            _view.NgayNhap.SelectedDate = DateTime.Now;

            // Act & Assert
            Assert.Throws<Exception>(() => _viewModel._SaveCommand(_view), "Chưa đủ thông tin");
        }
    }

}
