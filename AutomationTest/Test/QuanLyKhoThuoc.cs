using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using FlaUI.UIA3;
using System;
using System.Collections.Generic;
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
            if(elementInput == null)
            {
                Console.WriteLine("Không tìm thấy phần tử để nhập nội dung tìm kiếm");
                return;
            }

            //ví dụ tìm kiếm abc đi
            //trước khi tìm kiếm mình phải lấy toàn bộ list thuốc ban đầu đã
            List<string>listThuocRoot = GetListThuocCur(mainWindow);
            if(listThuocRoot == null)
            {
                Console.WriteLine("Không tìm thấy element hiển thị danh sách thuốc");
                return;
            }

            //nhập nội dung ô input tìm kiếm
            string noiDungTimKiem = "panadol";//ví dụ tìm panadol
            elementInput.Text = noiDungTimKiem;//ví dụ tìm abc đi(mọi người có thể linh hoạt dùng excel làm list testcase nha)


            //lấy danh sách hiển thị trên listview hiện tại
            Thread.Sleep(500);
            List<string>listThuocCur = GetListThuocCur(mainWindow);

            bool flag = false;
            if (CompareList(listThuocRoot.Where(n => n.ToLower().Contains(noiDungTimKiem.ToLower())).ToList(), listThuocCur))
            {
                flag = true;
            }
            Console.WriteLine($"Test case--{noiDungTimKiem}--{(flag?"đúng":"sai")}");
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
            if(list1 == null || list2 == null) return false;
            if (list1.Count != list2.Count) return false;

            for(int i = 0; i < list1.Count; i++)
            {
                if (list1[i] != list2[i])
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}
