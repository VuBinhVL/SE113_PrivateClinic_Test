﻿using AutomationTest.Helpers;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;
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
	public static class QuanLyKhoThuoc
	{
		#region tìm kiếm thuốc

		public static void TimKiemThuoc(Window mainWindow)
		{
			//lấy control textbox input tìm kiếm
			var elementInput = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("txbSearch"))?.AsTextBox();
			if (elementInput == null)
			{
				Console.WriteLine("Không tìm thấy phần tử để nhập nội dung tìm kiếm");
				return;
			}

			//ví dụ tìm kiếm abc đi
			//trước khi tìm kiếm mình phải lấy toàn bộ list thuốc ban đầu đã
			List<string> listThuocRoot = GetListThuocCur(mainWindow);
			if (listThuocRoot == null)
			{
				Console.WriteLine("Không tìm thấy element hiển thị danh sách thuốc");
				return;
			}

			//nhập nội dung ô input tìm kiếm
			string noiDungTimKiem = "panadol";//ví dụ tìm panadol
			elementInput.Text = noiDungTimKiem;//ví dụ tìm abc đi(mọi người có thể linh hoạt dùng excel làm list testcase nha)

			//lấy danh sách hiển thị trên listview hiện tại
			Thread.Sleep(500);
			List<string> listThuocCur = GetListThuocCur(mainWindow);

			bool flag = false;
			if (CompareList(listThuocRoot.Where(n => n.ToLower().Contains(noiDungTimKiem.ToLower())).ToList(), listThuocCur))
			{
				flag = true;
			}
			Console.WriteLine($"Test case--{noiDungTimKiem}--{(flag ? "đúng" : "sai")}");
		}

		//làm hàm riêng để tái sử dụng
		private static List<string> GetListThuocCur(Window mainWindow)
		{
			List<string> listThuocRoot = new List<string>();
			//tìm id của listview
			var listView = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("MedicineListView"))?.AsListBox();
			if (listView == null)
			{
				return null;
			}
			var listItem = listView.FindAllChildren();
			//danh sách item của listview
			foreach (var item in listItem)
			{
				//Console.WriteLine(item.Properties.Name.Value);
				var colsTextblock = item.FindAllDescendants(c => c.ByControlType(ControlType.Text));
				//cột 3 lưu tên
				for (int i = 0; i < colsTextblock.Length; i++)
				{
					if (i == 2)
					{
						listThuocRoot.Add(colsTextblock[i]?.Properties?.Name?.Value);
					}
				}
			}
			return listThuocRoot;
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

		#endregion tìm kiếm thuốc

		#region Thêm số lượng cho thuốc cũ

		public static void ThemSoLuongChoThuocCu(Window mainWindow)
		{
			// Thiết lập LicenseContext
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			// Đường dẫn file Excel
			string filePathRoot = @"..\..\TestCase\ThemSoLuongThuocCu\TestCase.xlsx";
			string filePathTemp = @"..\..\TestCase\ThemSoLuongThuocCu\TestCaseTemp.xlsx";
			File.Copy(filePathRoot, filePathTemp, true);
			// Đọc file Excel
			using (var package = new ExcelPackage(new FileInfo(filePathTemp)))
			{
				// Lấy worksheet đầu tiên
				var worksheet = package.Workbook.Worksheets[0];

				// Đọc dữ liệu từ các ô

				int rowStart = 2;
				int rowEnd = 7;

                int countFalse = 0;
                for (int row = rowStart; row <= rowEnd; row++)
                {
                    string medication = worksheet.Cells[row, 1]?.Text;
                    string quantity = worksheet.Cells[row, 2]?.Text;
                    string expectedResult = worksheet.Cells[row, 3]?.Text;
                    //Console.WriteLine(medication+"-"+quantity);
                    //nhấn vào nút thêm
                    MouseHelper.MoveAndLeftClick(1289, 290);
                    Utils.Sleep(1000);
                    mainWindow = Program.RefreshWindow();

					var elementComboboxListThuoc = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ChonThuoccbx"))?.AsComboBox();
					if (elementComboboxListThuoc == null)
					{
						Console.WriteLine("ComboBox danh sách thuốc không tìm thấy!");
						return;
					}

					if (!string.IsNullOrEmpty(medication))
					{
						// Mở ComboBox để hiển thị các mục
						elementComboboxListThuoc.Patterns.ExpandCollapse.Pattern.Expand();

						// Lấy danh sách các mục
						var items = elementComboboxListThuoc.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));

						// Tìm mục có giá trị cần chọn
						string valueToSelect = medication;
						var itemToSelect = items.FirstOrDefault(i => i.Name == valueToSelect);

                        if (itemToSelect != null)
                        {
                            itemToSelect.Patterns.SelectionItem.Pattern.Select();
                            Console.WriteLine($"Đã chọn giá trị: {valueToSelect}");
                        }
                        else
                        {
                            Console.WriteLine($"Không tìm thấy giá trị: {valueToSelect}");
                            return;
                        }
                        //MouseHelper.MoveAndLeftClick(580, 729);//thoát combobox
                        MouseHelper.MoveAndLeftClick(1124, 346);//thoát combobox
                        Utils.Sleep(1000);
                    }

                    MouseHelper.MoveAndLeftClick(634, 498);//nhấn vào ô nhập số lượng
                    Utils.Sleep(1000);
                    Keyboard.Type(quantity);
                    Utils.Sleep(1000);

                    //nhấn nút lưu
                    MouseHelper.MoveAndLeftClick(1074, 642);
                    Utils.Sleep(2000);

                    bool flag = false;
                    if(expectedResult== "Lưu thành công")
                    {
                        expectedResult = "Bạn muốn lưu thông tin cho thuốc ?";
                        flag = true;
                    }
                    var msg = ControlHelper.FindMessageBoxByContent(expectedResult);
                    if(flag)
                    {
                        //nhấn vào nút yes
                        MouseHelper.MoveAndLeftClick(698, 484);
                        Utils.Sleep(1000);
                    }
                    string result = "T";
                    if (msg == null)
                    {
                        result = "F";
                        countFalse++;
                    }
                    MouseHelper.MoveAndLeftClick(753, 483);//nhấn nút ok
                    worksheet.Cells[row, 4].Value= result;
                    MouseHelper.MoveAndLeftClick(1179, 118);
                    Utils.Sleep(2000);
                }

				worksheet.Cells[11, 2].Value = countFalse.ToString();
				worksheet.Cells[10, 2].Value = ((rowEnd - rowStart + 1) - countFalse).ToString();
				package.Save();
			}
		}
        #endregion 

        #region sửa thuốc
        public static void SuaThuoc(Window mainWindow)
        {
            // Thiết lập LicenseContext
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Đường dẫn file Excel
            string filePathRoot = @"..\..\TestCase\SuaThuoc\TestCase.xlsx";
            string filePathTemp = @"..\..\TestCase\SuaThuoc\TestCaseTemp.xlsx";
            File.Copy(filePathRoot, filePathTemp, true);
            // Đọc file Excel
            using (var package = new ExcelPackage(new FileInfo(filePathTemp)))
            {
                // Lấy worksheet đầu tiên
                var worksheet = package.Workbook.Worksheets[0];

                // Đọc dữ liệu từ các ô

                int rowStart = 2;
                int rowEnd = 7;

                int countFalse = 0;
                for (int row = rowStart; row <= rowEnd; row++)
                {
                    string tenthuoc = worksheet.Cells[row, 1]?.Text;//tên thuốc
                    string donvitinh = worksheet.Cells[row, 2]?.Text;
                    string price = worksheet.Cells[row, 3]?.Text;
                    string expectedResult = worksheet.Cells[row, 4]?.Text;
                    //Console.WriteLine(medication+"-"+quantity);
                    //nhấn vào nút sửa
                    MouseHelper.MoveAndLeftClick(1509, 637);
                    Utils.Sleep(1000);
                    mainWindow = Program.RefreshWindow();

                    var elementComboboxListDVT = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("TenDVTcbx"))?.AsComboBox();
                    if (elementComboboxListDVT == null)
                    {
                        Console.WriteLine("ComboBox đơn vị tính không tìm thấy!");
                        return;
                    }

                    MouseHelper.MoveAndLeftClick(752, 496);//nhấn vào ô tên thuốc
                    Utils.Sleep(1000);
                    // Gửi tổ hợp phím Ctrl + A để chọn toàn bộ nội dung
                    Keyboard.Press(VirtualKeyShort.CONTROL);
                    Keyboard.Type(VirtualKeyShort.KEY_A);
                    Keyboard.Release(VirtualKeyShort.CONTROL);

                    // Nhấn phím Delete để xóa
                    Keyboard.Type(VirtualKeyShort.DELETE);
                    Keyboard.Type(tenthuoc);
                    Utils.Sleep(1000);


                    if (!string.IsNullOrEmpty(donvitinh))
                    {
                        // Mở ComboBox để hiển thị các mục
                        elementComboboxListDVT.Patterns.ExpandCollapse.Pattern.Expand();

                        // Lấy danh sách các mục
                        var items = elementComboboxListDVT.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));

                        // Tìm mục có giá trị cần chọn
                        string valueToSelect = donvitinh;
                        var itemToSelect = items.FirstOrDefault(i => i.Name == valueToSelect);

                        if (itemToSelect != null)
                        {
                            itemToSelect.Patterns.SelectionItem.Pattern.Select();
                            Console.WriteLine($"Đã chọn giá trị: {valueToSelect}");
                        }
                        else
                        {
                            Console.WriteLine($"Không tìm thấy giá trị: {valueToSelect}");
                            return;
                        }
                        //MouseHelper.MoveAndLeftClick(580, 729);//thoát combobox
                        MouseHelper.MoveAndLeftClick(651, 665);//thoát combobox
                        Utils.Sleep(1000);
                    }
                    MouseHelper.MoveAndLeftClick(1180, 499);//nhấn vào ô giá nhập
                    Utils.Sleep(1000);
                    // Gửi tổ hợp phím Ctrl + A để chọn toàn bộ nội dung
                    Keyboard.Press(VirtualKeyShort.CONTROL);
                    Keyboard.Type(VirtualKeyShort.KEY_A);
                    Keyboard.Release(VirtualKeyShort.CONTROL);

                    // Nhấn phím Delete để xóa
                    Keyboard.Type(VirtualKeyShort.DELETE);
                    Keyboard.Type(price);
                    Utils.Sleep(1000);

                    //nhấn nút lưu
                    MouseHelper.MoveAndLeftClick(1261, 724);
                    Utils.Sleep(2000);

                    bool flag = false;
                    if (expectedResult == "Sửa thành công")
                    {
                        expectedResult = "Bạn muốn lưu thông tin thuốc?";
                        flag = true;
                    }
                    var msg = ControlHelper.FindMessageBoxByContent(expectedResult);
                    if (flag && msg !=null)
                    {
                        //nhấn vào nút yes
                        MouseHelper.MoveAndLeftClick(698, 484);
                        Utils.Sleep(1000);
                    }
                    string result = "T";
                    if (msg == null)
                    {
                        result = "F";
                        countFalse++;
                    }
                    else
                    {
                        MouseHelper.MoveAndLeftClick(950, 598);//nhấn nút ok
                    }
                    MouseHelper.MoveAndLeftClick(1061, 726);//nhấn nút hủy
                    worksheet.Cells[row, 5].Value = result;
                    Utils.Sleep(2000);
                }

                worksheet.Cells[11, 2].Value = countFalse.ToString();
                worksheet.Cells[10, 2].Value = ((rowEnd - rowStart + 1) - countFalse).ToString();
                package.Save();
            }
        }
        #endregion
    }
}