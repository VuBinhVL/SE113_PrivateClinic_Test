using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using PrivateClinic.ViewModel.QuanLiKhoThuocVM;
using System.Windows.Controls;

namespace PrivateClinic.UnitTest.QuyDinhThuoc
{     
[TestFixture]
    public class SuaDonViTinhViewModelTests
    {
        private SuaDonViTinhViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _viewModel = new SuaDonViTinhViewModel();
        }

        [Test]
        public void ExitCommand_ShouldExecute_WhenViewIsNotNull()
        {
            // Arrange
            var mockView = new Mock<SuaDonViTinhView>();

            // Act
            var canExecute = _viewModel.ExitCommand.CanExecute(mockView.Object);

            // Assert
            Assert.IsTrue(canExecute);
        }

        [Test]
        public void ExitCommand_ShouldNotExecute_WhenViewIsNull()
        {
            // Act
            var canExecute = _viewModel.ExitCommand.CanExecute(null);

            // Assert
            Assert.IsFalse(canExecute);
        }

        [Test]
        public void SwitchView_ShouldSetCorrectCurrentView_WhenUserControlNameIsThuoccu()
        {
            // Act
            _viewModel.SwitchViewCommand.Execute("Thuoccu");

            // Assert
            Assert.IsInstanceOf<ThemDonViTinhUS>(_viewModel.CurrentView);
        }

        [Test]
        public void SwitchView_ShouldSetCorrectCurrentView_WhenUserControlNameIsThuocmoi()
        {
            // Act
            _viewModel.SwitchViewCommand.Execute("Thuocmoi");

            // Assert
            Assert.IsInstanceOf<XoaDonViTinhUS>(_viewModel.CurrentView);
        }

        [Test]
        public void SwitchView_ShouldNotChangeCurrentView_WhenUserControlNameIsInvalid()
        {
            // Arrange
            var initialView = _viewModel.CurrentView;

            // Act
            _viewModel.SwitchViewCommand.Execute("InvalidName");

            // Assert
            Assert.AreEqual(initialView, _viewModel.CurrentView);
        }
    }

    // Mock class for SuaDonViTinhView
    public class SuaDonViTinhView
    {
        public void Close()
        {
            // Simulate closing a window
        }
    }

    // Mock class for ThemDonViTinhUS
    public class ThemDonViTinhUS : UserControl
    {
    }

    // Mock class for XoaDonViTinhUS
    public class XoaDonViTinhUS : UserControl
    {
    }
}

