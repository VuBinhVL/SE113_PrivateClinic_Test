using AutomationTest.Helpers;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;
using FlaUI.UIA3.Patterns;
using OfficeOpenXml;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;



namespace AutomationTest.Test
{
    public class QuanLiBenhNhan
    {
        #region tìm kiếm thuốc
        public static void TimKiemBenhNhan(Window mainWindow)
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
            List<string> listThuocRoot = GetListBNCur(mainWindow);
            if (listThuocRoot == null)
            {
                Console.WriteLine("Không tìm thấy element hiển thị danh sách bệnh nhân");
                return;
            }

            //nhập nội dung ô input tìm kiếm
            string noiDungTimKiem = "Tình";//ví dụ tìm Tình
            elementInput.Text = noiDungTimKiem;//ví dụ tìm abc đi(mọi người có thể linh hoạt dùng excel làm list testcase nha)


            //lấy danh sách hiển thị trên listview hiện tại
            Thread.Sleep(500);
            List<string> listThuocCur = GetListBNCur(mainWindow);

            bool flag = false;
            if (CompareList(listThuocRoot.Where(n => n.ToLower().Contains(noiDungTimKiem.ToLower())).ToList(), listThuocCur))
            {
                flag = true;
            }
            Console.WriteLine($"Test case--{noiDungTimKiem}--{(flag ? "đúng" : "sai")}");
        }
        //làm hàm riêng để tái sử dụng
        private static List<string> GetListBNCur(Window mainWindow)
        {
            List<string> listBNRoot = new List<string>();
            //tìm id của listview 
            var listView = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("ListViewBN"))?.AsListBox();
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
                //cột 2 lưu tên
                for (int i = 0; i < colsTextblock.Length; i++)
                {
                    if (i == 1)
                    {
                        listBNRoot.Add(colsTextblock[i]?.Properties?.Name?.Value);
                    }
                }
            }
            return listBNRoot;

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

        #endregion



        #region Thêm số lượng cho thuốc cũ
        public static void ThemThuocChoBenhNhan(Window mainWindow)
        {
            // Thiết lập LicenseContext
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Đường dẫn file Excel
            string filePathRoot = @"..\..\TestCase\ThemThuocChoBenhNhan\TestCase.xlsx";
            string filePathTemp = @"..\..\TestCase\ThemThuocChoBenhNhan\TestCaseTemp.xlsx";
            File.Copy(filePathRoot, filePathTemp, true);
            // Đọc file Excel
            using (var package = new ExcelPackage(new FileInfo(filePathTemp)))
            {
                // Lấy worksheet đầu tiên
                var worksheet = package.Workbook.Worksheets[0];

                // Đọc dữ liệu từ các ô

                int rowStart = 2;
                int rowEnd = 8;

                int countFalse = 0;
                //nhấn vào nút thêm
                MouseHelper.MoveAndLeftClick(1262, 498);
                Utils.Sleep(1000);
                mainWindow = Program.RefreshWindow();
                for (int row = rowStart; row <= rowEnd; row++)
                {
                    string medication = worksheet.Cells[row, 1]?.Text;
                    string quantity = worksheet.Cells[row, 2]?.Text;
                    string usage = worksheet.Cells[row, 3]?.Text;
                    string expectedResult = worksheet.Cells[row, 4]?.Text;                 


                    var elementComboboxListThuoc = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("cbxTenThuoc"))?.AsComboBox();
                    if (elementComboboxListThuoc == null)
                    {
                        Console.WriteLine("ComboBox danh sách thuốc không tìm thấy!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(medication))
                    {
                        // Mở ComboBox để hiển thị các mục
                        elementComboboxListThuoc.Patterns.ExpandCollapse.Pattern.Expand();
                        Utils.Sleep(1000); // Chờ UI cập nhật
                                           
                        var items = elementComboboxListThuoc.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                        string valueToSelect = medication;

                        var itemToSelect = (items.FirstOrDefault(i => (i.FindFirstDescendant(e=>e.ByControlType(ControlType.Text)).Properties.Name==medication)));

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
                        MouseHelper.MoveAndLeftClick(1081, 346);//thoát combobox
                        Utils.Sleep(1000);
                    }

                    MouseHelper.MoveAndLeftClick(1037, 225);//nhấn vào ô nhập số lượng
                    Utils.Sleep(1000);
                    Keyboard.Press(VirtualKeyShort.CONTROL);
                    Keyboard.Type("a"); // Chọn toàn bộ nội dung
                    Keyboard.Release(VirtualKeyShort.CONTROL);
                    Utils.Sleep(500);
                    Keyboard.Press(VirtualKeyShort.BACK); // Xóa nội dung
                    Utils.Sleep(500);
                    Keyboard.Type(quantity);
                    

                    var elementComboboxCachDung = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("cbxCachDung"))?.AsComboBox();
                    if (elementComboboxCachDung == null)
                    {
                        Console.WriteLine("ComboBox cách dùng không tìm thấy!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(usage))
                    {
                        // Mở ComboBox để hiển thị các mục
                        elementComboboxCachDung.Patterns.ExpandCollapse.Pattern.Expand();

                        // Lấy danh sách các mục
                        var items = elementComboboxCachDung.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));

                        // Tìm mục có giá trị cần chọn
                        string valueToSelect = usage;
                        var itemToSelect = (items.FirstOrDefault(i => (i.FindFirstDescendant(e => e.ByControlType(ControlType.Text)).Properties.Name == usage)));


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
                        MouseHelper.MoveAndLeftClick(1081, 346);//thoát combobox
                        Utils.Sleep(1000);
                    }

                    //nhấn nút thêm
                    MouseHelper.MoveAndLeftClick(1146, 446);
                    Utils.Sleep(2000);


                    // Tìm TextBlock hiển thị thông báo lỗi
                    var errorTextBlock = mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text)
                        .And(cf.ByAutomationId("ErrorMessage"))); // Chỉnh AutomationId nếu cần

                    string result = "F";

                    // Kiểm tra nội dung của TextBlock
                    if (errorTextBlock != null)
                    {
                        var actualError = errorTextBlock.Properties.Name;

                        // So sánh nội dung với expectedResult
                        if (!string.IsNullOrEmpty(actualError?.Value))
                        {
                            // Lấy giá trị thực tế và xử lý khoảng trắng
                            var expected = expectedResult.TrimEnd('.', ' ');
                            var actual = actualError.Value.TrimEnd('.', ' ');

                            if (expected.Equals(actual, StringComparison.Ordinal))
                            {
                                result = "T";
                            }
                            else
                            {
                                // In chuỗi dưới dạng mã ký tự để kiểm tra sự khác biệt
                                Console.WriteLine($"Expected: [{string.Join(", ", expected.Select(c => (int)c))}]");
                                Console.WriteLine($"But got:  [{string.Join(", ", actual.Select(c => (int)c))}]");
                                Console.WriteLine($"Expected (raw): {expected}, but got (raw): {actual}");
                            }
                        }


                    }
                    else
                    {
                        result = "T";
                        Console.WriteLine("Không tìm thấy TextBlock hiển thị lỗi!");
                    }

                    // Xuất kết quả
                    if (result == "T")
                    {
                        Console.WriteLine("Test passed.");
                    }
                    else
                    {
                        Console.WriteLine("Test failed.");
                        countFalse++;
                    }


                    //MouseHelper.MoveAndLeftClick(753, 483);//nhấn nút ok
                    worksheet.Cells[row, 5].Value = result;
                    //MouseHelper.MoveAndLeftClick(1179, 118);
                    Utils.Sleep(2000);
                }
                //nhấn nút lưu
                MouseHelper.MoveAndLeftClick(800, 446);
                Utils.Sleep(2000);
                //nhấn nút ok
                MouseHelper.MoveAndLeftClick(734, 486);
                Utils.Sleep(2000);

                worksheet.Cells[21, 2].Value = countFalse.ToString();
                worksheet.Cells[22, 2].Value = ((rowEnd - rowStart + 1) - countFalse).ToString();
                package.Save();
            }

        }
        #endregion
    }
}
