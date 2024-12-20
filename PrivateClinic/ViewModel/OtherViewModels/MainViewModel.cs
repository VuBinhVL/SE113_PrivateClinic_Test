using PrivateClinic.View.BangDieuKhien;
using PrivateClinic.View.HoSoBacSi;
using PrivateClinic.Model;
using PrivateClinic.View.OtherViews;
using PrivateClinic.View.QuanLiKhamBenh;
using PrivateClinic.View.QuanLiKhoThuoc;
using PrivateClinic.View.QuanLiTiepDon;
using PrivateClinic.View.ThanhToan;
using PrivateClinic.ViewModel.ThanhToan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrivateClinic.ViewModel.OtherViewModels
{
	public class MainViewModel : BaseViewModel
	{
		//field
		//field
		private NGUOIDUNG _NhanVien;

		private NGUOIDUNG _User;
		public static Frame MainFrame { get; set; }

		//Command ThanhToanCM
		public ICommand CloseLogin { get; set; }

		public ICommand MinimizeLogin { get; set; }
		public ICommand MoveWindow { get; set; }
		public ICommand BangDieuKhienCM { get; set; }
		public ICommand TiepDonCM { get; set; }
		public ICommand KhamBenhCM { get; set; }
		public ICommand ThanhToanCM { get; set; }
		public ICommand KhoThuocCM { get; set; }
		public ICommand BacSiCM { get; set; }
		public ICommand LogOutCM { get; set; }
		public ICommand LoadPageCM { get; set; }
		public ICommand Loadwd { get; set; }

		public ICommand HomeCM { get; set; }

		//property
		public NGUOIDUNG User
		{ get => _User; set { _User = value; OnPropertyChanged(); } }

		public NGUOIDUNG NhanVien
		{ get => _NhanVien; set { _NhanVien = value; OnPropertyChanged(); } }

		private Visibility _SetQuanLy;
		public Visibility SetQuanLy
		{ get => _SetQuanLy; set { _SetQuanLy = value; OnPropertyChanged(); } }
		private Visibility _SetNhanVien;
		public Visibility SetNhanVien
		{ get => _SetNhanVien; set { _SetNhanVien = value; OnPropertyChanged(); } }

		public MainViewModel()
		{
			CloseLogin = new RelayCommand<MainView>((p) => true, (p) => Close(p));
			MinimizeLogin = new RelayCommand<MainView>((p) => true, (p) => Minimize(p));
			MoveWindow = new RelayCommand<MainView>((p) => true, (p) => moveWindow(p));
			Loadwd = new RelayCommand<MainView>((p) => true, (p) => _Loadwd(p));

			LoadPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
			{
				MainFrame = p;
				p.Content = new View.QuanLiTiepDon.QuanLiKhamBenhView();
			});
			HomeCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
			{
				MainFrame.Content = new BangDieuKhienView();
			});

			BangDieuKhienCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
			{
				MainFrame.Content = new BangDieuKhienView();
			});

			TiepDonCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
			{
				MainFrame.Content = new View.QuanLiTiepDon.QuanLiKhamBenhView();
			});

			ThanhToanCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
			{
				var view = new ThanhToanView();
				var viewModel = new ThanhToanViewModel();
				viewModel.RefreshData();
				view.DataContext = viewModel;
				MainFrame.Content = view;
			});

			BacSiCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
			{
				MainFrame.Content = new HoSoBacSiView();
			});
			KhoThuocCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
			{
				MainFrame.Content = new QuanLiKhoThuocView();
			});

			KhamBenhCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
			{
				MainFrame.Content = new View.QuanLiKhamBenh.QuanLiKhamBenhView();
			});

			LogOutCM = new RelayCommand<FrameworkElement>((p) => { return p == null ? false : true; }, (p) =>
			{
				FrameworkElement window = GetParentWindow(p);
				var w = window as Window;
				if (w != null)
				{
					w.Hide();
					LoginView w1 = new LoginView();
					w1.ShowDialog(); // Hiển thị cửa sổ LoginView
					w.Close();
				}
			});
		}

		private FrameworkElement GetParentWindow(FrameworkElement p)
		{
			FrameworkElement parent = p;

			while (parent.Parent != null)
			{
				parent = parent.Parent as FrameworkElement;
			}
			return parent;
		}

		private void _Loadwd(MainView p)
		{
			if (LoginViewModel.IsLogin)
			{
				string a = Const.TenDangNhap;
				User = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TenDangNhap == a).FirstOrDefault();
				Const.PQ = new PHANQUYEN();

				if (User.MaNhom == 1)
				{
					Const.PQ.MaNhom = 1;

					SetQuanLy = Visibility.Visible;
					SetNhanVien = Visibility.Collapsed;
				}
				else
				{
					Const.PQ.MaNhom = 2;
					SetQuanLy = Visibility.Collapsed;
					SetNhanVien = Visibility.Visible;
				}

				_LoadUsername(p);
			}
		}

		public void moveWindow(MainView p)
		{
			p.DragMove();
		}

		public void Close(MainView p)
		{
			System.Windows.Application.Current.Shutdown();
		}

		public void Minimize(MainView p)
		{
			p.WindowState = WindowState.Minimized;
		}

		private void _LoadUsername(MainView p)
		{
			//p.TenDangNhap.Text = string.Join(" ", NGUOIDUNG.HoTen.Split().Reverse().Take(2).Reverse());
		}

		private void _LoadQuyen(MainView p)
		{
			//p.Quyen.Text = User.QTV ? "Quản lý" : "Nhân viên";
		}
	}
}