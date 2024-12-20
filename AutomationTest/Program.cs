using AutomationTest.Helpers;
using AutomationTest.Test;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationTest
{
	public class Program
	{
		private static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;

			// Đường dẫn đến file .exe của ứng dụng WPF
			var appPath = @"..\..\..\PrivateClinic\bin\Debug\PrivateClinic.exe";

			using (var app = FlaUI.Core.Application.Launch(appPath))
			{
				using (var automation = new UIA3Automation())
				{
					_app = app;
					_automation = automation;

					//phần login này mn cứ giữ nguyên mặc định dị

					#region login

					// Lấy cửa sổ chính của ứng dụng WPF
					var mainWindow = app.GetMainWindow(automation);
					Console.WriteLine("Đã mở cửa sổ chính của ứng dụng.");

					// Tìm các phần tử UI của màn hình Login
					var usernameBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("txtTenDangNhap"))?.AsTextBox();
					var passwordBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("FloatingPasswordBox"))?.AsTextBox();
					var loginButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("btnDangNhap"))?.AsButton();

					// Kiểm tra các phần tử không null
					if (usernameBox == null || passwordBox == null || loginButton == null)
					{
						Console.WriteLine("Không tìm thấy các phần tử UI. Kiểm tra AutomationId.");
						return;
					}

					// Nhập Username và Password
					usernameBox.Enter("admin01");
					passwordBox.Enter("123456");
					Console.WriteLine("Đã nhập thông tin đăng nhập.");

					// Click nút Login
					loginButton.Invoke();
					Console.WriteLine("Đã bấm nút Login.");

					// Kiểm tra kết quả sau khi đăng nhập
					//   Console.ReadLine();
					Utils.Sleep(3000);//chỗ này dừng luồng hiện tại 3s là vì khi nhấn nút đăng nhập hệ thống đợi load nữa

					//    //mình phải check xem login có thành công k đã, thì để ý rằng khi login thành công nó sẽ có thanh navigation như vậy
					//    //thì mẹo check là mình sẽ check nếu màn hình xuất hiện nút đăng xuất là đã đăng nhập thành công
					//    //nút đăng xuất có id là 6
					mainWindow = RefreshWindow();//refresh lại window hiện tại của app
					var btnDangXuat = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("6"))?.AsButton();
					if (btnDangXuat == null)
					{
						//login thất bại
						Console.WriteLine("Login thất bại");
						return;
					}
					//    //vào trang chủ của app

					#endregion login

					Console.WriteLine("Đang ở trang chủ");
					////    Console.WriteLine("Nhấn phím bất kì để start");
					////    Console.ReadKey();//nhấn phím bất kì để start

					#region quản lý kho thuốc

					//vào trang quản lý kho thuốc
					//MouseHelper.MoveAndLeftClick(502, 645);
					//Thread.Sleep(1000);//ngủ 1s cho nó load csdl
					//mainWindow = RefreshWindow();

					//tìm kiếm thuốc
					//   QuanLyKhoThuoc.TimKiemThuoc(mainWindow);

					//thêm thuốc
					//QuanLyKhoThuoc.ThemSoLuongChoThuocCu( mainWindow);

					#endregion quản lý kho thuốc

					#region Hồ sơ bác sĩ

					var hoSoView = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("5"))?.AsButton();
					// Click nút HoSoBacSi
					if (hoSoView == null)
					{
						Console.WriteLine("Không tìm thấy nút hồ sơ bác sĩ");
						return;
					}
					hoSoView.Click();
					Console.WriteLine("Đã vào trang Hồ sơ bác sĩ.");

					//Vào phần đổi mật khẩu
					//HoSoBacSi.DoiMatKhau(mainWindow);

					//Tìm kiếm bác sĩ
					HoSoBacSi.TimKiem(mainWindow);

					#endregion Hồ sơ bác sĩ
				}
			}
		}

		private static Application _app;
		private static UIA3Automation _automation;

		public static Window RefreshWindow()
		{
			return _app.GetMainWindow(_automation);
		}

		public static Window[] GetWindowsCur()
		{
			return _app.GetAllTopLevelWindows(_automation);
		}
	}
}