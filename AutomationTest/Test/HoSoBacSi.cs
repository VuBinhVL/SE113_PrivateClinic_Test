using AutomationTest.Helpers;
using FlaUI.Core.AutomationElements;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTest.Test
{
	public static class HoSoBacSi
	{
		#region Đổi mật khẩu

		public static void DoiMatKhau(Window mainWindow)
		{
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
					Utils.Sleep(2000);
				}

				worksheet.Cells[12, 2].Value = countFalse.ToString();
				worksheet.Cells[11, 2].Value = ((rowEnd - rowStart + 1) - countFalse).ToString();
				package.Save();
				Console.WriteLine("Đã test xong");
			}
		}

		#endregion Đổi mật khẩu
	}
}