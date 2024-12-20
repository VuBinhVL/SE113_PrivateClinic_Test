using AutomationTest.Helpers;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationTest.Test
{
	public static class HoSoBacSi
	{
		#region Đổi mật khẩu

		public static void DoiMatKhau(Window mainWindow)
		{
			//Vào trang đổi mật khẩu
			Utils.Sleep(1000);
			mainWindow = Program.RefreshWindow();
			var doiMKview = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("btnDoiMK"))?.AsButton();
			if (doiMKview == null)
			{
				Console.WriteLine("Không tìm thấy nút hồ sơ bác sĩ");
				return;
			}
			doiMKview.Invoke();
			Console.WriteLine("Đã vào trang Đổi mật khẩu.");
			// Thiết lập LicenseContext
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			// Đường dẫn file Excel
			string filePathRoot = @"..\..\TestCase\DoiMatKhau\TestCase.xlsx";
			string filePathTemp = @"..\..\TestCase\DoiMatKhau\TestCaseTemp.xlsx";
			File.Copy(filePathRoot, filePathTemp, true);

			//Lấy các textbox và nút
			var matkhaucuTextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("txtMKcu"))?.AsTextBox();
			var matkhaumoi1TextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("txtMKmoi"))?.AsTextBox();
			var matkhaumoi2TextBox = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("txtNhapLai"))?.AsTextBox();
			var doiMatKhauButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("btnDMK"))?.AsButton();
			var messageButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("btnOk"))?.AsButton();
			// Đọc file Excel
			using (var package = new ExcelPackage(new FileInfo(filePathTemp)))
			{
				// Lấy worksheet đầu tiên
				var worksheet = package.Workbook.Worksheets[0];

				// Đọc dữ liệu từ các ô

				int rowStart = 2;
				int rowEnd = 8;

				int countFalse = 0;
				for (int row = rowStart; row <= rowEnd; row++)
				{
					string matkhaucu = worksheet.Cells[row, 1]?.Text;
					string matkhaumoi1 = worksheet.Cells[row, 2]?.Text;
					string matkhaummoi2 = worksheet.Cells[row, 3]?.Text;
					string expectedResult = worksheet.Cells[row, 4]?.Text;

					//Nhập thông tin cho view và nhấn nút đổi mật khẩu
					matkhaucuTextBox.Text = string.Empty; // Xóa nội dung cũ
					matkhaucuTextBox.Enter(matkhaucu);
					Utils.Sleep(1000);

					matkhaumoi1TextBox.Text = string.Empty; // Xóa nội dung cũ
					matkhaumoi1TextBox.Enter(matkhaumoi1);
					Utils.Sleep(1000);

					matkhaumoi2TextBox.Text = string.Empty; // Xóa nội dung cũ
					matkhaumoi2TextBox.Enter(matkhaummoi2);
					Utils.Sleep(1000);

					doiMatKhauButton.Invoke();
					Utils.Sleep(2000);

					bool flag = false;
					if (expectedResult == "Đổi mật khẩu thành công")
					{
						flag = true;
					}
					var msg = ControlHelper.FindMessageBoxByContent(expectedResult);
					//Kiểm tra kết quả sau khi test
					string result = "T";
					if (msg == null)
					{
						result = "F";
						countFalse++;
					}
					MouseHelper.MoveAndLeftClick(761, 491);//nhấn nút ok
					worksheet.Cells[row, 5].Value = result;
					Utils.Sleep(1000);
				}

				worksheet.Cells[12, 2].Value = countFalse.ToString();
				worksheet.Cells[11, 2].Value = ((rowEnd - rowStart + 1) - countFalse).ToString();
				package.Save();
				Console.WriteLine("Đã test xong");
			}
		}

		#endregion Đổi mật khẩu

		#region Tìm bác sĩ

		public static void TimKiem(Window mainWindow)
		{
			//Vào trang danh sách bác sĩ
			var danhSachBSview = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("btnDSBS"))?.AsButton();
			if (danhSachBSview == null)
			{
				Console.WriteLine("Không tìm thấy nút danh sách bác sĩ");
				return;
			}
			danhSachBSview.Invoke();
			Console.WriteLine("Đã vào danh sách bác sĩ");

			// Thiết lập LicenseContext
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			// Đường dẫn file Excel
			string filePathRoot = @"..\..\TestCase\TimKiemBacSi\TestCase.xlsx";
			string filePathTemp = @"..\..\TestCase\TimKiemBacSi\TestCaseTemp.xlsx";
			File.Copy(filePathRoot, filePathTemp, true);

			//lấy control textbox input tìm kiếm
			var elementInput = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("txtTimKiem"))?.AsTextBox();
			if (elementInput == null)
			{
				Console.WriteLine("Không tìm thấy phần tử để nhập nội dung tìm kiếm");
				return;
			}

			//trước khi tìm kiếm mình phải lấy toàn bộ list bác sĩ ban đầu đã
			List<string> listBacSiRoot = GetListBacSiCur(mainWindow);
			if (listBacSiRoot == null)
			{
				Console.WriteLine("Không tìm thấy element hiển thị danh sách bác sĩ");
				return;
			}

			using (var package = new ExcelPackage(new FileInfo(filePathTemp)))
			{
				// Lấy worksheet đầu tiên
				var worksheet = package.Workbook.Worksheets[0];

				// Đọc dữ liệu từ các ô

				int rowStart = 2;
				int rowEnd = 4;

				int countFalse = 0;
				for (int row = rowStart; row <= rowEnd; row++)
				{
					string noiDungTimKiem = worksheet.Cells[row, 1]?.Text;

					elementInput.Text = string.Empty;
					elementInput.Text = noiDungTimKiem;
					Utils.Sleep(1000);

					//lấy danh sách hiển thị trên listview hiện tại
					Thread.Sleep(500);
					List<string> listBacSiCur = GetListBacSiCur(mainWindow);

					if (CompareList(listBacSiRoot.Where(n => n.ToLower().Contains(noiDungTimKiem.ToLower())).ToList(), listBacSiCur))
					{
						worksheet.Cells[row, 3].Value = "T";
					}
					else
					{
						worksheet.Cells[row, 3].Value = "F";
						countFalse++;
					}
					Utils.Sleep(1000);
				}

				worksheet.Cells[8, 2].Value = countFalse.ToString();
				worksheet.Cells[7, 2].Value = ((rowEnd - rowStart + 1) - countFalse).ToString();
				package.Save();
				Console.WriteLine("Đã test xong");
			}
		}

		private static List<string> GetListBacSiCur(Window mainWindow)
		{
			List<string> listBacSiRoot = new List<string>();
			//tìm id của listview
			var listView = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ListViewBS"))?.AsListBox();
			if (listView == null)
			{
				return null;
			}
			var listItem = listView.FindAllChildren();
			//danh sách item của listview
			foreach (var item in listItem)
			{
				var colsTextblock = item.FindAllDescendants(c => c.ByControlType(ControlType.Text));
				listBacSiRoot.Add(colsTextblock[0]?.Properties?.Name?.Value);
			}
			return listBacSiRoot;
		}

		//2 list = nhau thì trả về true
		private static bool CompareList(List<string> list1, List<string> list2)
		{
			if (list1 == null || list2 == null) return false;
			if (list1.Count != list2.Count) return false;

			for (int i = 0; i < list1.Count; i++)
			{
				if (list1[i] != list2[i])
				{
					return false;
				}
			}
			return true;
		}

		#endregion Tìm bác sĩ
	}
}